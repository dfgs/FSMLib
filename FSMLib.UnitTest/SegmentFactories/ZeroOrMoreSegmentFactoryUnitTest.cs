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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedNodeContainer(), new MockedNodeConnector(),new MockedPredicate<char>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			ZeroOrMoreSegmentFactory<char> factory;

			factory = new ZeroOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeConnector(), new ZeroOrMore<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedNodeContainer(), null, new ZeroOrMore<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedNodeContainer(), new MockedNodeConnector(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			ZeroOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			ZeroOrMore<char> predicate;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new ZeroOrMoreSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new One<char>() { Value='a' } );
			sequence.Items.Add(new One<char>() { Value = 'b' });
			sequence.Items.Add(new One<char>() { Value = 'c' });

			predicate = new ZeroOrMore<char>() {  Item=sequence};

			segment = factory.BuildSegment(graph, connector, predicate);
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
			ZeroOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			ZeroOrMore<char> predicate;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new ZeroOrMoreSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new One<char>() { Value = 'a' });
			or.Items.Add(new One<char>() { Value = 'b' });
			or.Items.Add(new One<char>() { Value = 'c' });

			predicate = new ZeroOrMore<char>() { Item = or };

			segment = factory.BuildSegment(graph, connector, predicate);
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
