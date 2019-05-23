using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class GraphParser<T>
	{
		private Graph<T> graph;
		private int nodeIndex;

		public int TransitionCount
		{
			get { return graph.Nodes[nodeIndex].TerminalTransitions.Count+ graph.Nodes[nodeIndex].NonTerminalTransitions.Count; }
		}

		
		public GraphParser(Graph<T> Graph)
		{
			this.graph = Graph;this.nodeIndex = 0;

		}

		public bool Parse(T Input, int MatchIndex = 0)
		{
			int index = 0;

			foreach(TerminalTransition<T> transition in graph.Nodes[nodeIndex].TerminalTransitions)
			{
				if (transition.Match(Input))
				{
					if (index == MatchIndex)
					{
						nodeIndex = transition.TargetNodeIndex;
						return true;
					}
					index++;
				}
			}
			return false;
		}

		public void Reset()
		{
			nodeIndex = 0;
		}

	}
}
