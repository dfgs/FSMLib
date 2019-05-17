using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public class Automaton<T>:IAutomaton<T>
	{
		private Graph<T> graph;
		private int nodeIndex;

		private Stack<IInput<T>> inputStack;
		private Stack<int> nodeIndexStack;

		public int StackCount
		{
			get => inputStack.Count;
		}

		public Automaton(Graph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
			inputStack = new Stack<IInput<T>>();
			nodeIndexStack = new Stack<int>();
			nodeIndex = 0;
		}

		public void Reset()
		{
			nodeIndex = 0;
			inputStack.Clear();
			nodeIndexStack.Clear();
		}

		public bool Feed(IInput<T> Item)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach(Transition<T> transition in node.Transitions.OrderBy(item=>item.Input.Priority))	// must match input with lower priority first
			{
				if (transition.Input.Match(Item))
				{
					nodeIndexStack.Push(nodeIndex);
					inputStack.Push(Item);
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}

			return false;
		}
		public bool Feed(T Item)
		{
			return Feed(new TerminalInput<T>() { Value=Item });
		}

		public bool CanReduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			return node.MatchedRules.Count > 0;
		}

		public string Reduce()
		{
			Node<T> node;
			IInput<T> input;
			MatchedRule matchedRule;

			node = graph.Nodes[nodeIndex];
			if (node.MatchedRules.Count == 0) return null;

			matchedRule = node.MatchedRules[0];

			while (inputStack.Count>0 &&  (!graph.Nodes[nodeIndex].RootIDs.Contains(matchedRule.ID)) )
			{
				nodeIndex = nodeIndexStack.Pop();
				input=inputStack.Pop();
			}

			return matchedRule.Name;
			
		}


	}
}
