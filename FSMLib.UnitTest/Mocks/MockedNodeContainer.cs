using FSMLib.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedNodeContainer : INodeContainer
	{
		public Node CreateNode()
		{
			throw new NotImplementedException();
		}

		public int GetNodeIndex(Node Node)
		{
			throw new NotImplementedException();
		}

		public Node GetTargetNode(Transition Transition)
		{
			throw new NotImplementedException();
		}
	}
}
