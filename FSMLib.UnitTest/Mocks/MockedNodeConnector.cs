using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedNodeConnector : INodeConnector<char>
	{
		

		public void Connect(IEnumerable<Node<char>> Nodes, IEnumerable<Transition<char>> Transitions)
		{
			throw new NotImplementedException();
		}
	}
}
