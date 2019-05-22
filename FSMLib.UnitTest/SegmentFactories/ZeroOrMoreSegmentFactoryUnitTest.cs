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
	public class ZeroOrMoreSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ZeroOrMoreSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			ZeroOrMoreSegmentFactory<char> factory;

			factory = new ZeroOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			ZeroOrMoreSegmentFactory<char> factory;

			factory = new ZeroOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null,  new ZeroOrMore<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  new ZeroOrMore<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			ZeroOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			ZeroOrMore<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new ZeroOrMoreSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new ZeroOrMore<char>() {  Item=sequence};

			segment = factory.BuildSegment(context,  predicate, new TerminalTransition<char>() { Value = 'd' }.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(2, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(2, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[0].Match('d'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			ZeroOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			ZeroOrMore<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new ZeroOrMoreSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new Terminal<char>() { Value = 'a' });
			or.Items.Add(new Terminal<char>() { Value = 'b' });
			or.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new ZeroOrMore<char>() { Item = or };

			segment = factory.BuildSegment(context, predicate, new TerminalTransition<char>() { Value='d' }.AsEnumerable()  );
			Assert.IsNotNull(segment);
			Assert.AreEqual(4, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(4, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[0].Match('d'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[1].Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[2].Match('b'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[3].Match('c'));

		}

	}
}
