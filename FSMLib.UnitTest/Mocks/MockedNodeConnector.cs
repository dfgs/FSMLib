using FSMLib.Graphs;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedNodeConnector : INodeConnector
	{
		public void Connect(IEnumerable<Node> InputNodes, IEnumerable<Node> TargetNodes)
		{
			throw new NotImplementedException();
		}
	}
}
