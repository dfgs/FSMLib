using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class Automaton<T>:IAutomaton<T>
	{
		private ActionTable<T> actionTable;
		private int stateIndex;

		private Stack<BaseNode<T>> nodeStack;
		private Stack<int> stateIndexStack;


		public int StackCount
		{
			get => nodeStack.Count;
		}

		public Automaton(ActionTable<T> ActionTable)
		{
			if (ActionTable == null) throw new ArgumentNullException("ActionTable");
			this.actionTable = ActionTable;
			nodeStack = new Stack<BaseNode<T>>();
			stateIndexStack = new Stack<int>();
			stateIndex = 0;
		}

		public void Reset()
		{
			stateIndex = 0;
			nodeStack.Clear();
			stateIndexStack.Clear();
		}

		

		private bool Feed(TerminalNode<T> Node)
		{
			State<T> state;

			state = actionTable.States[stateIndex];
			foreach (ShiftOnTerminal<T> action in state.TerminalActions)
			{
				if (action.Match(Node.Value))
				{
					stateIndexStack.Push(stateIndex);
					nodeStack.Push(Node);
					stateIndex = action.TargetStateIndex;
					return true;
				}
			}
			return false;
		}

		private bool Feed(NonTerminalNode<T> Node)
		{
			State<T> state;

			state = actionTable.States[stateIndex];
			foreach (ShifOnNonTerminal<T> action in state.NonTerminalActions)
			{
				if (action.Match(Node.Name))
				{
					stateIndexStack.Push(stateIndex);
					nodeStack.Push(Node);
					stateIndex = action.TargetStateIndex;
					return true;
				}
			}
			return false;
		}

		private NonTerminalNode<T> Reduce(string Name,int TargetStateIndex)
		{
			BaseNode<T> baseNode;
			NonTerminalNode<T> reducedNode;

			reducedNode = new NonTerminalNode<T>() { Name = Name };

			while (nodeStack.Count > 0 && (stateIndex != TargetStateIndex))
			{
				stateIndex = stateIndexStack.Pop();
				baseNode = nodeStack.Pop();
				reducedNode.States.Insert(0, baseNode);  // stack order is inverted compared to state childs
			}

			return reducedNode;
		}
		private NonTerminalNode<T> Reduce(TerminalNode<T> Node)
		{
			State<T> state;
			ReductionTarget<T> target;

			state = actionTable.States[stateIndex];

			foreach (Reduce<T> action in state.ReductionActions)
			{
				target = action.Targets.FirstOrDefault(item =>  Node.Value.Equals(item.Value));
				if (target == null) continue;

				return Reduce(action.Name, target.TargetStateIndex);
			}
			return null;
		}

		public void Feed(T Input)
		{
			TerminalNode<T> inputNode;
			NonTerminalNode<T> nonTerminalNode;

			inputNode = new TerminalNode<T>() { Value=Input };

			while (true)
			{
				if (Feed(inputNode)) return;

				nonTerminalNode = Reduce(inputNode);
				if ((nonTerminalNode == null) || (!Feed(nonTerminalNode))) throw new AutomatonException<T>(Input, nodeStack);
			}		

		}


		/*private bool CanReduce()
		{
			State<T> state;

			state = actionTable.States[stateIndex];
			return state.ReductionActions.Count > 0;
		}*/



		public bool CanAccept()
		{
			State<T> state;
			Accept<T> axiom;

			state = actionTable.States[stateIndex];
			axiom = state.AcceptActions.FirstOrDefault();

			return axiom != null;
		}

		public NonTerminalNode<T> Accept()
		{
			State<T> state;
			Accept<T> axiom;

			state = actionTable.States[stateIndex];
			axiom = state.AcceptActions.FirstOrDefault();

			if (axiom==null) throw new InvalidOperationException("Automaton cannot accept in current state");
			return Reduce( axiom.Name,0 );
		}


	}
}
