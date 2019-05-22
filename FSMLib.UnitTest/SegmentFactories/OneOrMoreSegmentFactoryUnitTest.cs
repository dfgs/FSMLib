﻿using FSMLib.Graphs;
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
	public class OneOrMoreSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OneOrMoreSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), new MockedPredicate<char>(),Enumerable.Empty<ReductionTransition<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null, new OneOrMore<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedGraphFactoryContext(),  new OneOrMore<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			OneOrMore<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);

			factory = new OneOrMoreSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() {  Item=sequence};

			segment = factory.BuildSegment(context,  predicate,Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[0].Match('a'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			OneOrMore<char> predicate;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new OneOrMoreSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new Terminal<char>() { Value = 'a' });
			or.Items.Add(new Terminal<char>() { Value = 'b' });
			or.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() { Item = or };

			segment = factory.BuildSegment( context, predicate, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(3, segment.Outputs.First().TerminalTransitions.Count);

			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[0].Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[1].Match('b'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalTransitions[2].Match('c'));

		}

	}
}
