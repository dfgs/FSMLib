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
		O0 -a-> O1 -b-> O2 -c-> O3 
	*/

	[ExcludeFromCodeCoverage]
	public class TestGraph5:Graph<char>
	{
		public TestGraph5()
		{
			for(int t=0;t<4;t++) this.CreateNode();

			Nodes[3].RecognizedRules.Add("A");

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } });
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 2, Input = new TerminalInput<char>() { Value = 'b' } });
			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 3, Input = new TerminalInput<char>() { Value = 'c' } });
		}

	}
}
