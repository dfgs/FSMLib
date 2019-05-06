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
		private int longuestStackSize;
		private int acceptedInputCount;

		private Stack<IInput<T>> inputStack;

		public int StackCount
		{
			get => inputStack.Count;
		}

		public Automaton(Graph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
			inputStack = new Stack<IInput<T>>();
			nodeIndex = 0;
			longuestStackSize = 0;
			acceptedInputCount = 0;
		}

		public void Reset()
		{
			nodeIndex = 0;
			longuestStackSize = 0;
			acceptedInputCount = 0;
			inputStack.Clear();
		}

		public bool Feed(IInput<T> Item)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach(Transition<T> transition in node.Transitions.OrderBy(item=>item.Input.Priority))	// must match input with lower priority first
			{
				if (transition.Input.Match(Item))
				{
					acceptedInputCount++;
					nodeIndex = transition.TargetNodeIndex;
					inputStack.Push(Item);
					if (CanReduce()) longuestStackSize = StackCount;
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
			return node.RecognizedRules.Count > 0;
		}

		public string Reduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			if (node.RecognizedRules.Count == 0) return null;
			for (int t = 0; t < acceptedInputCount; t++)
			{
				inputStack.Pop();
			}
			acceptedInputCount = 0;
			return node.RecognizedRules[0];
			
		}


	}
}
