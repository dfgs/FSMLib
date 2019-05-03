﻿using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedNodeConnector : INodeConnector<char>
	{
		

		public void Connect( IEnumerable<Node<char>> Nodes, IEnumerable<BaseTransition<char>> Transitions)
		{
			throw new NotImplementedException();
		}
	}
}
