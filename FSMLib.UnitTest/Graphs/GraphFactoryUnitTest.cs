﻿using System;
using FSMLib.Graphs;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs
{
	[TestClass]
	public class GraphFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>(null, new MockedSegmentFactoryProvider<char>(),new MockedSituationProducer() ));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>(new MockedNodeConnector(), null, new MockedSituationProducer()));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>(new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>(),null));
		}

		[TestMethod]
		public void ShouldBuildGraphFromBasicSequence()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule;
			Sequence<char> predicate;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(),new SituationProducer<char>() ) ;

			predicate = new char[] { 'a', 'b', 'c'};
			rule = new Rule<char>();
			rule.Predicate = predicate;

			graph = factory.BuildGraph(rule.AsEnumerable());
			Assert.IsNotNull(graph);
			Assert.AreEqual(4, graph.Nodes.Count);

			Assert.AreEqual(1, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(1, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[1].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(1, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(3, graph.Nodes[2].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(0, graph.Nodes[3].Transitions.Count);

		}
		[TestMethod]
		public void ShouldBuildGraphFromTwoSequences()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule1,rule2;
			Sequence<char> predicate;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule1 = new Rule<char>();
			rule1.Predicate = predicate;
			predicate = new char[] { 'a', 'b', 'c' };
			rule2 = new Rule<char>();
			rule2.Predicate = predicate;

			graph = factory.BuildGraph(new Rule<char>[] { rule1,rule2 });
			Assert.IsNotNull(graph);
			Assert.AreEqual(7, graph.Nodes.Count);

			Assert.AreEqual(2, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(4, graph.Nodes[0].Transitions[1].TargetNodeIndex);

			// segment from rule 1
			Assert.AreEqual(1, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[1].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(1, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(3, graph.Nodes[2].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(0, graph.Nodes[3].Transitions.Count);//*/

			// segment from rule 2
			Assert.AreEqual(1, graph.Nodes[4].Transitions.Count);
			Assert.AreEqual(5, graph.Nodes[4].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(1, graph.Nodes[5].Transitions.Count);
			Assert.AreEqual(6, graph.Nodes[5].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(0, graph.Nodes[6].Transitions.Count);//*/

		}
		[TestMethod]
		public void ShouldBuildGraphFromBasicOr()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule;
			Or<char> predicate;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			graph = factory.BuildGraph(rule.AsEnumerable());
			Assert.IsNotNull(graph);
			Assert.AreEqual(4, graph.Nodes.Count);

			Assert.AreEqual(3, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, graph.Nodes[0].Transitions[1].TargetNodeIndex);
			Assert.AreEqual(3, graph.Nodes[0].Transitions[2].TargetNodeIndex);
			Assert.AreEqual(0, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[3].Transitions.Count);

		}

		[TestMethod]
		public void ShouldNotBuildGraphWhenNullRulesAreProvided()
		{
			GraphFactory<char> factory;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(()=> factory.BuildGraph(null));
		}

		[TestMethod]
		public void ShouldNotBuildDeterministicGraphWhenNullParameterIsProvided()
		{
			GraphFactory<char> factory;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildDeterministicGraph(null));
		}
		[TestMethod]
		public void ShouldBuildDeterministicGraphFromEmptyBaseGraph()
		{
			GraphFactory<char> factory;
			Graph<char> graph;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph = factory.BuildDeterministicGraph(new Graph<char>());
			Assert.IsNotNull(graph);
			Assert.AreEqual(0, graph.Nodes.Count);
		}
		[TestMethod]
		public void ShouldBuildDeterministicGraphFromSimpleBaseGraph()
		{
			GraphFactory<char> factory;
			Graph<char> baseGraph,graph;

			factory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());


			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				   -a-> O4 -b-> O5|
				   -a-> O6 -c-> O7
			*/
			baseGraph = new MockedGraph();

			graph = factory.BuildDeterministicGraph(baseGraph);
			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				          |c-> O7
			*/

			Assert.IsNotNull(graph);
			Assert.AreEqual(0, graph.Nodes.Count);


		}
	}


}
