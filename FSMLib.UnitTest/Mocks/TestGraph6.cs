using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	/*
		O0 -a-> O1 -a-> O2 -a-> O3 
		           -.-> O4 -a-> O5 
	*/

	[ExcludeFromCodeCoverage]
	public class TestGraph6:Graph<char>
	{
		public TestGraph6()
		{
			for(int t=0;t<6;t++) this.Nodes.Add(new Node<char>());

			Nodes[3].MatchedRules.Add(new MatchedRule() { Name = "A", IsAxiom = true });
			Nodes[5].MatchedRules.Add(new MatchedRule() { Name = "B" });

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } });

			// any input must be placed first in order to be the first to match input
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 4, Input = new AnyTerminalInput<char>() });
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 2, Input = new TerminalInput<char>() { Value = 'a' } });

			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 3, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[4].Transitions.Add(new Transition<char>() { TargetNodeIndex = 5, Input = new TerminalInput<char>() { Value = 'a' } });

		}

	}
}
