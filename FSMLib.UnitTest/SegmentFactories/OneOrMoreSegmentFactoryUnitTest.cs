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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(),new MockedPredicate<char>(),Transition<char>.Termination.AsEnumerable()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeContainer(), new MockedNodeConnector(), new OneOrMore<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", null, new MockedNodeConnector(), new OneOrMore<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), null, new OneOrMore<char>(), Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), null, Transition<char>.Termination.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment("rule", new MockedNodeContainer(), new MockedNodeConnector(), new OneOrMore<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			OneOrMore<char> predicate;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new OneOrMoreSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new One<char>() { Value='a' } );
			sequence.Items.Add(new One<char>() { Value = 'b' });
			sequence.Items.Add(new One<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() {  Item=sequence};

			segment = factory.BuildSegment("rule", graph, connector, predicate,Transition<char>.Termination.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(1, segment.Outputs.First().Transitions.Count);

			Assert.AreEqual(true, segment.Inputs.First().Input.Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().Transitions[0].Input.Match('a'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			OneOrMore<char> predicate;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new OneOrMoreSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new One<char>() { Value = 'a' });
			or.Items.Add(new One<char>() { Value = 'b' });
			or.Items.Add(new One<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() { Item = or };

			segment = factory.BuildSegment("rule", graph, connector, predicate, Transition<char>.Termination.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(3, segment.Outputs.First().Transitions.Count);

			Assert.AreEqual(true, segment.Inputs.First().Input.Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().Transitions[0].Input.Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().Transitions[1].Input.Match('b'));
			Assert.AreEqual(true, segment.Outputs.First().Transitions[2].Input.Match('c'));

		}

	}
}
