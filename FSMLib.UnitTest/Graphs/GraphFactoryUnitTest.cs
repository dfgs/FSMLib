using System;
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
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>(null, (container, connector) => new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>((container) => new MockedNodeConnector(), null));
		}

		[TestMethod]
		public void ShouldBuildGraphFromBasicSequence()
		{
			GraphFactory<char> factory;
			Graph graph;
			Rule<char> rule;
			Sequence<char> predicate;

			factory = new GraphFactory<char>( (container)=>new NodeConnector(container), (container,connector) => new SegmentFactoryProvider<char>(container,connector) ) ;

			predicate = new char[] { 'a', 'b', 'c'};
			rule = new Rule<char>();
			rule.Predicate = predicate;

			graph = factory.BuildGraph(new Rule<char>[] { rule });
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
		public void ShouldBuildGraphFromBasicOr()
		{
			GraphFactory<char> factory;
			Graph graph;
			Rule<char> rule;
			Or<char> predicate;

			factory = new GraphFactory<char>((container) => new NodeConnector(container), (container, connector) => new SegmentFactoryProvider<char>(container, connector));

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			graph = factory.BuildGraph(new Rule<char>[] { rule });
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

			factory = new GraphFactory<char>((container) => new NodeConnector(container), (container, connector) => new SegmentFactoryProvider<char>(container, connector)) ;

			Assert.ThrowsException<ArgumentNullException>(()=> factory.BuildGraph(null));
		}
	}


}
