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
	public class SequenceSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SequenceSegmentFactory<char>(null, new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SequenceSegmentFactory<char>(new MockedNodeContainer(), null, new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SequenceSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedPredicate<char>()));
		}
		[TestMethod]
		public void ShouldFailWithNullPredicate()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			SequenceSegmentFactory<char> factory;
			Segment segment;
			Graph graph;
			NodeConnector connector;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;

			graph = new Graph();
			connector = new NodeConnector(graph);
			provider = new SegmentFactoryProvider<char>(graph, connector);
			factory = new SequenceSegmentFactory<char>(graph, connector, provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new One<char>());
			sequence.Items.Add(new One<char>());
			sequence.Items.Add(new One<char>());

			segment = factory.BuildSegment(sequence);
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
		}


	}
}
