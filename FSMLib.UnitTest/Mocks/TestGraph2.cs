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
		   -a-> O4 -b-> O5 -d-> O6 
	*/

	[ExcludeFromCodeCoverage]
	public class TestGraph2:Graph<char>
	{
		public TestGraph2()
		{
			for(int t=0;t<7;t++) this.CreateNode();

			Nodes[3].RecognizedRules.Add("A");
			Nodes[6].RecognizedRules.Add("B");


			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 2, Input = new OneInput<char>() { Value = 'b' } });
			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 3, Input = new OneInput<char>() { Value = 'c' } });

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 4, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[4].Transitions.Add(new Transition<char>() { TargetNodeIndex = 5, Input = new OneInput<char>() { Value = 'b' } });
			Nodes[5].Transitions.Add(new Transition<char>() { TargetNodeIndex = 6, Input = new OneInput<char>() { Value = 'd' } });

		}
	}
}
