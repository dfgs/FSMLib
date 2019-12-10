using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Automatons;
using FSMLib.Common.Inputs;
using FSMLib.Common.Table;
using FSMLib.Common.Actions;
using FSMLib.Common.Automatons;
using FSMLib.Attributes;

namespace FSMLib.Common.Automatons
{
	public abstract class Automaton<T>:IAutomaton<T>
	{
		private readonly IAutomatonTable<T> automatonTable;
		private int stateIndex;

		private readonly AutomatonStack<IBaseNode<T>> nodeStack;
		private readonly AutomatonStack<int> stateIndexStack;


		public int StackCount
		{
			get => nodeStack.Count;
		}

		public Automaton(IAutomatonTable<T> AutomatonTable)
		{
			if (AutomatonTable == null) throw new ArgumentNullException("AutomatonTable");
			this.automatonTable = AutomatonTable;
			nodeStack = new AutomatonStack<IBaseNode<T>>();
			stateIndexStack = new AutomatonStack<int>();
			stateIndex = 0;
		}

		protected abstract ITerminalInput<T> OnCreateTerminalInput(T Value);
		protected abstract INonTerminalInput<T> OnCreateNonTerminalInput(string Name);

		public void Reset()
		{
			stateIndex = 0;
			nodeStack.Clear();
			stateIndexStack.Clear();
		}

		

		private bool Shift(INonTerminalNode<T> Node)
		{
			IState<T> state;
			IShift<T> action;

			state = automatonTable.GetState(stateIndex);
			action = state.GetShift(Node.Input);
			if (action == null) return false;

			stateIndexStack.Push(stateIndex);
			nodeStack.Push(Node);
			stateIndex = action.TargetStateIndex;
			return true;
		}

		private bool Shift(ITerminalNode<T> Node)
		{
			IState<T> state;
			IShift<T> action;

			state = automatonTable.GetState(stateIndex);
			action = state.GetShift(Node.Input);
			if (action == null) return false;

			stateIndexStack.Push(stateIndex);
			nodeStack.Push(Node);
			stateIndex = action.TargetStateIndex;
			return true;
		}

		private INonTerminalNode<T> Reduce(string Name,IEnumerable<IRuleAttribute> Attributes)
		{
			IBaseNode<T> baseNode;
			NonTerminalNode<T> reducedNode;

			reducedNode = new NonTerminalNode<T>(OnCreateNonTerminalInput(Name ) );
			reducedNode.Attributes.AddRange(Attributes);
			while (nodeStack.Count > 0)
			{
				stateIndex = stateIndexStack.Pop();
				baseNode = nodeStack.Pop();
				reducedNode.Nodes.Insert(0, baseNode);  // stack order is inverted compared to state childs

				if (CanFeed(Name)) break;
			}

			return reducedNode;
		}


		
		private INonTerminalNode<T> Reduce(TerminalNode<T> Node)
		{
			IState<T> state;
			IReduce<T> action;

			state = automatonTable.GetState(stateIndex);
			action = state.GetReduce(Node.Input);
			if (action == null) return null;

			return Reduce(action.Name,action.Attributes);
		}

		private bool CanFeed(IActionInput<T> Input)
		{
			IState<T> state;

			state = automatonTable.GetState(stateIndex);
			// check if we can shift
			if (state.GetShift(Input) != null) return true;
			// check if we can reduce and shift
			if (state.GetReduce(Input) != null) return true;

			return false;
		}

		public bool CanFeed(T Input)
		{
			return CanFeed(OnCreateTerminalInput(Input));
		}
		private bool CanFeed(string Name)
		{
			return CanFeed(OnCreateNonTerminalInput(Name));
		}



		private void Feed(ITerminalInput<T> Input)
		{
			TerminalNode<T> inputNode;
			INonTerminalNode<T> nonTerminalNode;

			inputNode = new TerminalNode<T>() { Input = Input };

			while (true)
			{
				if (Shift(inputNode)) return;

				nonTerminalNode = Reduce(inputNode);
				if ((nonTerminalNode == null) || (!Shift(nonTerminalNode))) throw new AutomatonException<T>(Input, nodeStack);
			}

		}

		public void Feed(T Value)
		{
			ITerminalInput<T> input;

			input = OnCreateTerminalInput(Value);
			Feed(input);
		}
		



		public bool CanAccept()
		{
			IState<T> state;
			IReduce<T> action;

			state = automatonTable.GetState(stateIndex);
			//axiom = state.AcceptActions.FirstOrDefault();

			action = state.GetReduce(new EOSInput<T>()); 

			return action!=null;
		}

		public INonTerminalNode<T> Accept()
		{
			INonTerminalNode<T> nonTerminalNode;
			EOSInput<T> eosInput;
			IState<T> state;
			IReduce<T> action;

			if (!CanAccept()) throw new InvalidOperationException("Automaton cannot accept in current state");

			eosInput= new EOSInput<T>(); 

			while (true)
			{

				state = automatonTable.GetState(stateIndex);
				action = state.GetReduce(eosInput);
				if (action == null) throw new AutomatonException<T>(null, nodeStack);

				nonTerminalNode=Reduce(action.Name,action.Attributes);
				if ((action.IsAxiom) && (this.StackCount==0)) return nonTerminalNode;

				if ((nonTerminalNode == null) || (!Shift(nonTerminalNode))) throw new AutomatonException<T>(null, nodeStack);
			}

		}


	}
}
