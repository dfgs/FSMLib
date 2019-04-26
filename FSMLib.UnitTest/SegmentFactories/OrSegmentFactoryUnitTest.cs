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
	public class OrSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OrSegmentFactory<char>(null, new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new OrSegmentFactory<char>(new MockedNodeContainer(), null, new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new OrSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedPredicate<char>()));
		}
		[TestMethod]
		public void ShouldFailWithNullPredicate()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OrSegmentFactory<char> factory;
			Segment segment;
			Graph graph;
			NodeConnector connector;
			SegmentFactoryProvider<char> provider;
			Or<char> Or;

			graph = new Graph();
			connector = new NodeConnector(graph);
			provider = new SegmentFactoryProvider<char>(graph, connector);
			factory = new OrSegmentFactory<char>(graph, connector, provider);

			Or = new Or<char>();
			Or.Items.Add(new One<char>());
			Or.Items.Add(new One<char>());
			Or.Items.Add(new One<char>());

			segment = factory.BuildSegment(Or);
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
		}



	}
}
