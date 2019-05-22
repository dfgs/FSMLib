using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null,  new Sequence<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  new Sequence<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			SequenceSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new SequenceSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context, sequence, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));

		}


	}
}
