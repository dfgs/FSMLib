using FSMLib.Graphs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedGraphFactoryContext : IGraphFactoryContext<char>
	{
		public Node<char> CreateNode()
		{
			throw new NotImplementedException();
		}

		public int GetNodeIndex(Node<char> Node)
		{
			throw new NotImplementedException();
		}

		public Node<char> GetTargetNode(Transition<char> Transition)
		{
			throw new NotImplementedException();
		}
	}
}
