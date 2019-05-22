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
	public class OptionalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OptionalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OptionalSegmentFactory<char> factory;

			factory = new OptionalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OptionalSegmentFactory<char> factory;

			factory = new OptionalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null,  new Optional<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(), null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  new Optional<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OptionalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			Optional<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);

			factory = new OptionalSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new Optional<char>() {  Item=sequence};

			segment = factory.BuildSegment(context,  predicate, new TerminalTransition<char>() { Value = 'd' }.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(2, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OptionalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			Optional<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new OptionalSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new Terminal<char>() { Value = 'a' });
			or.Items.Add(new Terminal<char>() { Value = 'b' });
			or.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new Optional<char>() { Item = or };

			segment = factory.BuildSegment(context,  predicate, new TerminalTransition<char>() { Value = 'd' }.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(4, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
			

		}

	}
}
