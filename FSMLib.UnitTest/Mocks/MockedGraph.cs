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
	public class MockedGraph:Graph<char>
	{
		public MockedGraph(params char[] Values)
		{

			Nodes.Add(new Node<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Nodes.Add(new Node<char>());
				Nodes[t].TerminalTransitions.Add(new TerminalTransition<char>() { Value = Values[t], TargetNodeIndex = t+1 });
			}

		}
		public MockedGraph(params string[] Values)
		{
			Nodes.Add(new Node<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Nodes.Add(new Node<char>());
				Nodes[t].NonTerminalTransitions.Add(new NonTerminalTransition<char>() { Name = Values[t], TargetNodeIndex = t + 1 });
			}
		}

	}
}
