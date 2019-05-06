using FSMLib.Graphs;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class AnyTerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AnyTerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			AnyTerminalSegmentFactory<char> factory;

			factory = new AnyTerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment( new MockedNodeContainer() ,new MockedNodeConnector(), new MockedPredicate<char>(),new EORTransition<char>().AsEnumerable() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			AnyTerminalSegmentFactory<char> factory;

			factory = new AnyTerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeConnector(), new AnyTerminal<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), null, new AnyTerminal<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), new MockedNodeConnector(), null, new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), new MockedNodeConnector(), new AnyTerminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			AnyTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new AnyTerminalSegmentFactory<char>( provider);

			segment=factory.BuildSegment( graph, connector, new AnyTerminal<char>(), new EORTransition<char>().AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.First()).Input.Match('a'));
		}



	}
}
