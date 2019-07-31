using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Automatons;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis.Automatons
{
	public class Automaton:IAutomaton<char>
	{
		private readonly AutomatonTable<char> automatonTable;
		private int stateIndex;

		private readonly AutomatonStack<BaseNode<char>> nodeStack;
		private readonly AutomatonStack<int> stateIndexStack;


		public int StackCount
		{
			get => nodeStack.Count;
		}

		public Automaton(AutomatonTable<char> AutomatonTable)
		{
			if (AutomatonTable == null) throw new ArgumentNullException("AutomatonTable");
			this.automatonTable = AutomatonTable;
			nodeStack = new AutomatonStack<BaseNode<char>>();
			stateIndexStack = new AutomatonStack<int>();
			stateIndex = 0;
		}

		public void Reset()
		{
			stateIndex = 0;
			nodeStack.Clear();
			stateIndexStack.Clear();
		}

		

		private bool Shift(NonTerminalNode<char> Node)
		{
			State<char> state;
			Shift<char> action;

			state = automatonTable.States[stateIndex];
			action = state.GetShift(Node.Input);
			if (action == null) return false;

			stateIndexStack.Push(stateIndex);
			nodeStack.Push(Node);
			stateIndex = action.TargetStateIndex;
			return true;
		}

		private bool Shift(TerminalNode<char> Node)
		{
			State<char> state;
			Shift<char> action;

			state = automatonTable.States[stateIndex];
			action = state.GetShift(Node.Input);
			if (action == null) return false;

			stateIndexStack.Push(stateIndex);
			nodeStack.Push(Node);
			stateIndex = action.TargetStateIndex;
			return true;
		}

		private NonTerminalNode<char> Reduce(string Name)
		{
			BaseNode<char> baseNode;
			NonTerminalNode<char> reducedNode;

			reducedNode = new NonTerminalNode<char>(new NonTerminalInput(Name ) );

			while (nodeStack.Count > 0)
			{
				stateIndex = stateIndexStack.Pop();
				baseNode = nodeStack.Pop();
				reducedNode.Nodes.Insert(0, baseNode);  // stack order is inverted compared to state childs

				if (CanFeed(Name)) break;
			}

			return reducedNode;
		}


		
		private NonTerminalNode<char> Reduce(TerminalNode<char> Node)
		{
			State<char> state;
			Reduce<char> action;

			state = automatonTable.States[stateIndex];
			action = state.GetReduce(Node.Input);
			if (action == null) return null;

			return Reduce(action.Name);
		}

		private bool CanFeed(IActionInput<char> Input)
		{
			State<char> state;

			state = automatonTable.States[stateIndex];
			// check if we can shift
			if (state.GetShift(Input) != null) return true;
			// check if we can reduce and shift
			if (state.GetReduce(Input) != null) return true;

			return false;
		}

		public bool CanFeed(char Input)
		{
			return CanFeed(new LetterInput(Input));
		}
		private bool CanFeed(string Name)
		{
			return CanFeed(new NonTerminalInput(Name));
		}



		private void Feed(ITerminalInput<char> Input)
		{
			TerminalNode<char> inputNode;
			NonTerminalNode<char> nonTerminalNode;

			inputNode = new TerminalNode<char>() { Input = Input };

			while (true)
			{
				if (Shift(inputNode)) return;

				nonTerminalNode = Reduce(inputNode);
				if ((nonTerminalNode == null) || (!Shift(nonTerminalNode))) throw new AutomatonException<char>(Input, nodeStack);
			}

		}

		public void Feed(char Value)
		{
			ITerminalInput<char> input;

			input = new LetterInput(Value);
			Feed(input);
		}
		



		public bool CanAccept()
		{
			State<char> state;
			Reduce<char> action;

			state = automatonTable.States[stateIndex];
			//axiom = state.AcceptActions.FirstOrDefault();

			action = state.GetReduce(new EOSInput<char>()); 

			return action!=null;
		}

		public NonTerminalNode<char> Accept()
		{
			NonTerminalNode<char> nonTerminalNode;
			EOSInput<char> eosInput;
			State<char> state;
			Reduce<char> action;

			if (!CanAccept()) throw new InvalidOperationException("Automaton cannot accept in current state");

			eosInput= new EOSInput<char>(); 

			while (true)
			{

				state = automatonTable.States[stateIndex];
				action = state.GetReduce(eosInput);
				if (action == null) throw new AutomatonException<char>(eosInput, nodeStack);

				nonTerminalNode=Reduce(action.Name);
				if (action.IsAxiom) return nonTerminalNode;

				if ((nonTerminalNode == null) || (!Shift(nonTerminalNode))) throw new AutomatonException<char>(eosInput, nodeStack);
			}

		}


	}
}
