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
	public class OneSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OneSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OneSegmentFactory<char> factory;

			factory = new OneSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment("rule", new MockedNodeContainer() ,new MockedNodeConnector(), new MockedPredicate<char>(), Transition<char>.Termination.AsEnumerable()));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OneSegmentFactory<char> factory;

			factory = new OneSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeContainer(), new MockedNodeConnector(), new One<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", null, new MockedNodeConnector(), new One<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), null, new One<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), null, Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), new One<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OneSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new OneSegmentFactory<char>( provider);

			segment=factory.BuildSegment("rule", graph, connector, new One<char>() { Value='a' }, Transition<char>.Termination.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			Assert.AreEqual(true, segment.Inputs.First().Input.Match('a'));
		}



	}
}
