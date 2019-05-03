using FSMLib.Graphs;
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

		public Automaton(Graph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
			nodeIndex = 0;
		}

		public void Reset()
		{
			nodeIndex = 0;
		}

		public bool Feed(T Item)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach(Transition<T> transition in node.Transitions.OrderBy(item=>item.Input.Priority))	// must match input with lower priority first
			{
				if (transition.Input.Match(Item))
				{
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}

			return false;
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
			if (node.RecognizedRules.Count > 0) return node.RecognizedRules[0];
			else return null;
		}


	}
}
