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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), new MockedNodeConnector(),new MockedPredicate<char>(), new EORTransition<char>().AsEnumerable()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null, new MockedNodeConnector(), new Sequence<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(), null, new Sequence<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(), new MockedNodeConnector(), null, new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(), new MockedNodeConnector(), new Sequence<char>(), null));
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
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(graph);
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new SequenceSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context, connector, sequence, new EORTransition<char>().AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);

			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.First()).Input.Match('a'));

		}


	}
}
