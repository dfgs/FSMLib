using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	/*
		       |---- a ---|   
		       |          |   
		O0 -a-> O1 -b-> O2|-c-> O3 
		   -a-> O4 -b-> O5|
		   -a-> O6 -c-> O7
	*/

	public class MockedGraph:Graph<char>
	{
		public MockedGraph()
		{
			for(int t=0;t<8;t++) this.CreateNode();

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[1].Transitions.Add(new Transition<char>() { TargetNodeIndex = 2, Input = new OneInput<char>() { Value = 'b' } });
			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 3, Input = new OneInput<char>() { Value = 'c' } });

			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 4, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[4].Transitions.Add(new Transition<char>() { TargetNodeIndex = 5, Input = new OneInput<char>() { Value = 'b' } });


			Nodes[0].Transitions.Add(new Transition<char>() { TargetNodeIndex = 6, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[6].Transitions.Add(new Transition<char>() { TargetNodeIndex = 7, Input = new OneInput<char>() { Value = 'c' } });

			Nodes[2].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			Nodes[5].Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });

		}
	}
}
