using FSMLib.Graphs;
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
		public void Connect(INodeContainer<char> NodeContainer, IEnumerable<Node<char>> InputNodes, IEnumerable<Node<char>> TargetNodes)
		{
			throw new NotImplementedException();
		}
	}
}
