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
			Assert.ThrowsException<ArgumentNullException>(() => new SequenceSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(),new MockedPredicate<char>(), Transition<char>.Termination.AsEnumerable()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeContainer(), new MockedNodeConnector(), new Sequence<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", null, new MockedNodeConnector(), new Sequence<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), null, new Sequence<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), null, Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), new Sequence<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			SequenceSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new SequenceSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new One<char>() { Value='a' } );
			sequence.Items.Add(new One<char>() { Value = 'b' });
			sequence.Items.Add(new One<char>() { Value = 'c' });

			segment = factory.BuildSegment("rule", graph, connector, sequence, Transition<char>.Termination.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);

			Assert.AreEqual(true, segment.Inputs.First().Input.Match('a'));

		}


	}
}
