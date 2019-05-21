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
		       |---- a ---|   
		       |          |   
		O0 -a-> O1 -{B}-> O2|-c-> O3 
		   -a-> O4 -{B}-> O5|
		   -a-> O6 -c-> O7
	*/

	[ExcludeFromCodeCoverage]
	public class TestGraph7:Graph<char>
	{
		public TestGraph7()
		{
			for(int t=0;t<8;t++) this.Nodes.Add(new Node<char>());
			Nodes[3].MatchedRules.Add(new MatchedRule() { Name = "A", IsAxiom = true });
			Nodes[5].MatchedRules.Add(new MatchedRule() { Name = "B" });
			Nodes[7].MatchedRules.Add(new MatchedRule() { Name = "C" });

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 2, Input = new NonTerminalInput<char>() { Name = "B" } });
			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 3, Input = new TerminalInput<char>() { Value = 'c' } });

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 4, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[4].Transitions.Add(new Transition<char>() { TargetNodeIndex = 5, Input = new NonTerminalInput<char>() { Name = "B" } });


			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 6, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[6].Transitions.Add(new Transition<char>() { TargetNodeIndex = 7, Input = new TerminalInput<char>() { Value = 'c' } });

			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[5].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } });

		}
	}
}
