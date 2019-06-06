using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;

namespace FSMLib.Tables
{
	public class Automaton<T>:IAutomaton<T>
	{
		private AutomatonTable<T> automatonTable;
		private int stateIndex;

		private AutomatonStack<BaseNode<T>> nodeStack;
		private AutomatonStack<int> stateIndexStack;


		public int StackCount
		{
			get => nodeStack.Count;
		}

		public Automaton(AutomatonTable<T> AutomatonTable)
		{
			if (AutomatonTable == null) throw new ArgumentNullException("AutomatonTable");
			this.automatonTable = AutomatonTable;
			nodeStack = new AutomatonStack<BaseNode<T>>();
			stateIndexStack = new AutomatonStack<int>();
			stateIndex = 0;
		}

		public void Reset()
		{
			stateIndex = 0;
			nodeStack.Clear();
			stateIndexStack.Clear();
		}

		

		private bool Feed(BaseNode<T> Node)
		{
			State<T> state;

			state = automatonTable.States[stateIndex];
			foreach (Shift<T> action in state.ShiftActions)
			{
				if (action.Input.Match(Node.Input))
				{
					stateIndexStack.Push(stateIndex);
					nodeStack.Push(Node);
					stateIndex = action.TargetStateIndex;
					return true;
				}
			}
			return false;
		}

		

		private NonTerminalNode<T> Reduce(string Name)
		{
			BaseNode<T> baseNode;
			NonTerminalNode<T> reducedNode;

			reducedNode = new NonTerminalNode<T>() { Input=new NonTerminalInput<T>() { Name=Name } };

			while (nodeStack.Count > 0)
			{
				stateIndex = stateIndexStack.Pop();
				baseNode = nodeStack.Pop();
				reducedNode.Nodes.Insert(0, baseNode);  // stack order is inverted compared to state childs

				if (CanFeed(Name)) break;
			}

			return reducedNode;
		}


		private int GetFirstIndexOnStack(int[] PossibleTargets)
		{
			for(int t=stateIndexStack.Count-1;t>=0;t--)
			{
				if (PossibleTargets.Contains(stateIndexStack[t])) return stateIndexStack[t];
			}
			return 0;
		}


		private NonTerminalNode<T> Reduce(TerminalNode<T> Node)
		{
			State<T> state;
			

			state = automatonTable.States[stateIndex];

			foreach (Reduce<T> action in state.ReductionActions)
			{
				if (!action.Input.Match(Node.Input)) continue;
				return Reduce(action.Name);
			}
			return null;
		}

		private bool CanFeed(BaseInput<T> Input)
		{
			State<T> state;

			state = automatonTable.States[stateIndex];
			// check if we can shift
			if (state.ShiftActions.FirstOrDefault(item => item.Input.Match(Input)) != null) return true;
			// check if we can reduce and shift
			if (state.ReductionActions.FirstOrDefault(item => item.Input.Match(Input)) != null) return true;

			return false;
		}

		public bool CanFeed(T Input)
		{
			return CanFeed(new TerminalInput<T>() { Value = Input });
		}
		private bool CanFeed(string Name)
		{
			return CanFeed(new NonTerminalInput<T>() { Name= Name});
		}



		private void Feed(BaseTerminalInput<T> Input)
		{
			TerminalNode<T> inputNode;
			NonTerminalNode<T> nonTerminalNode;

			inputNode = new TerminalNode<T>() { Input = Input };

			while (true)
			{
				if (Feed(inputNode)) return;

				nonTerminalNode = Reduce(inputNode);
				if ((nonTerminalNode == null) || (!Feed(nonTerminalNode))) throw new AutomatonException<T>(Input, nodeStack);
			}

		}

		public void Feed(T Value)
		{
			TerminalInput<T> input;

			input = new TerminalInput<T>() { Value = Value };
			Feed(input);
		}
		



		public bool CanAccept()
		{
			State<T> state;
			Reduce<T> action;

			state = automatonTable.States[stateIndex];
			//axiom = state.AcceptActions.FirstOrDefault();

			action = state.ReductionActions.FirstOrDefault(item => item.Input is EOSInput<T>);

			return action != null;
		}

		public NonTerminalNode<T> Accept()
		{
			NonTerminalNode<T> nonTerminalNode;

			if (!CanAccept()) throw new InvalidOperationException("Automaton cannot accept in current state");

			Feed(new EOSInput<T>());

			nonTerminalNode = nodeStack.OfType<NonTerminalNode<T>>().FirstOrDefault();

			return nonTerminalNode;
		}


	}
}
