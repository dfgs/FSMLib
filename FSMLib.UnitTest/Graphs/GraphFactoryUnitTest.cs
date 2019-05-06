using System;
using System.Linq;
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
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>( null, new MockedSituationProducer()));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactory<char>( new MockedSegmentFactoryProvider<char>(),null));
		}
		

		[TestMethod]
		public void ShouldBuildGraphFromBasicSequence()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule;
			Sequence<char> predicate;
			GraphParser<char> parser;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(),new SituationProducer<char>() ) ;

			predicate = new char[] { 'a', 'b', 'c'};
			rule = new Rule<char>();
			rule.Predicate = predicate;

			graph = factory.BuildGraph(rule.AsEnumerable());
			Assert.IsNotNull(graph);
			Assert.AreEqual(4, graph.Nodes.Count);

			parser = new GraphParser<char>(graph);

			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.TransitionCount);

		}
		[TestMethod]
		public void ShouldBuildGraphFromTwoSequences()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule1,rule2;
			Sequence<char> predicate;
			GraphParser<char> parser;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule1 = new Rule<char>();
			rule1.Predicate = predicate;
			predicate = new char[] { 'a', 'b', 'c' };
			rule2 = new Rule<char>();
			rule2.Predicate = predicate;

			graph = factory.BuildGraph(new Rule<char>[] { rule1,rule2 });
			Assert.IsNotNull(graph);
			Assert.AreEqual(7, graph.Nodes.Count);

			parser = new GraphParser<char>(graph);

			Assert.AreEqual(2, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.TransitionCount);

			parser.Reset();
			Assert.AreEqual(2, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('a',1));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.TransitionCount);

		}

		[TestMethod]
		public void ShouldBuildGraphFromBasicOr()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule;
			Or<char> predicate;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

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
		public void ShouldBuildGraphFromExtendedSequence()
		{
			GraphFactory<char> factory;
			Graph<char> graph;
			Rule<char> rule;
			Sequence<char> predicate;
			GraphParser<char> parser;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>() { Name="rule" };
			rule.Predicate = new ZeroOrMore<char>() { Item = predicate };

			graph = factory.BuildGraph(rule.AsEnumerable());
			Assert.IsNotNull(graph);
			Assert.AreEqual(4, graph.Nodes.Count);

			parser = new GraphParser<char>(graph);

			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(1, parser.TransitionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, graph.Nodes[0].RecognizedRules.Count);
			Assert.AreEqual("rule", graph.Nodes[0].RecognizedRules[0]);
		}
		[TestMethod]
		public void ShouldNotBuildGraphWhenNullRulesAreProvided()
		{
			GraphFactory<char> factory;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(()=> factory.BuildGraph(null));
		}

		[TestMethod]
		public void ShouldNotBuildDeterministicGraphWhenNullParameterIsProvided()
		{
			GraphFactory<char> factory;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildDeterministicGraph(null));
		}
		[TestMethod]
		public void ShouldBuildDeterministicGraphFromEmptyBaseGraph()
		{
			GraphFactory<char> factory;
			Graph<char> graph;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph = factory.BuildDeterministicGraph(new Graph<char>());
			Assert.IsNotNull(graph);
			Assert.AreEqual(0, graph.Nodes.Count);
		}


		[TestMethod]
		public void ShouldBuildDeterministicGraphFromTestGraph1()
		{
			GraphFactory<char> factory;
			Graph<char> baseGraph,graph;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			baseGraph = new TestGraph1();
			graph = factory.BuildDeterministicGraph(baseGraph);
			Assert.IsNotNull(graph);
			Assert.AreEqual(4, graph.Nodes.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[3].Transitions.Count);
			Assert.IsTrue(graph.Nodes[0].Transitions[0].Input.Match('a'));
			Assert.IsTrue(graph.Nodes[1].Transitions[0].Input.Match('b'));
			Assert.IsTrue(graph.Nodes[2].Transitions[0].Input.Match('c'));

			for (int t = 0; t < 3; t++) Assert.AreEqual(0, graph.Nodes[t].RecognizedRules.Count);

			Assert.AreEqual(2, graph.Nodes[3].RecognizedRules.Count);
		}

		[TestMethod]
		public void ShouldBuildDeterministicGraphFromTestGraph2()
		{
			GraphFactory<char> factory;
			Graph<char> baseGraph, graph;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			baseGraph = new TestGraph2();
			graph = factory.BuildDeterministicGraph(baseGraph);
			Assert.IsNotNull(graph);
			Assert.AreEqual(5, graph.Nodes.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[3].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[4].Transitions.Count);
			Assert.IsTrue(graph.Nodes[0].Transitions[0].Input.Match('a'));
			Assert.IsTrue(graph.Nodes[1].Transitions[0].Input.Match('b'));
			Assert.IsTrue(graph.Nodes[2].Transitions[0].Input.Match('c'));
			Assert.IsTrue(graph.Nodes[2].Transitions[1].Input.Match('d'));

			for (int t = 0; t < 3; t++) Assert.AreEqual(0, graph.Nodes[t].RecognizedRules.Count);

			Assert.AreEqual(1, graph.Nodes[3].RecognizedRules.Count);
			Assert.AreEqual(1, graph.Nodes[4].RecognizedRules.Count);

		}
		[TestMethod]
		public void ShouldBuildDeterministicGraphFromTestGraph3()
		{
			GraphFactory<char> factory;
			Graph<char> baseGraph, graph;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			baseGraph = new TestGraph3();
			graph = factory.BuildDeterministicGraph(baseGraph);
			Assert.IsNotNull(graph);
			Assert.AreEqual(6, graph.Nodes.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[3].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[4].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[5].Transitions.Count);
			Assert.IsTrue(graph.Nodes[0].Transitions[0].Input.Match('a'));
			Assert.IsTrue(graph.Nodes[1].Transitions[0].Input.Match('b'));
			Assert.IsTrue(graph.Nodes[1].Transitions[1].Input.Match('c'));
			Assert.IsTrue(graph.Nodes[2].Transitions[0].Input.Match('a'));
			Assert.IsTrue(graph.Nodes[3].Transitions[0].Input.Match('a'));


			for (int t = 0; t < 4; t++) Assert.AreEqual(0, graph.Nodes[t].RecognizedRules.Count);

			Assert.AreEqual(1, graph.Nodes[5].RecognizedRules.Count);
			Assert.AreEqual(1, graph.Nodes[4].RecognizedRules.Count);

		}


		[TestMethod]
		public void ShouldBuildDeterministicGraphFromTestGraph4()
		{
			GraphFactory<char> factory;
			Graph<char> baseGraph, graph;

			factory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			baseGraph = new TestGraph4();
			graph = factory.BuildDeterministicGraph(baseGraph);
			Assert.IsNotNull(graph);
			Assert.AreEqual(6, graph.Nodes.Count);
			Assert.AreEqual(1, graph.Nodes[0].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[1].Transitions.Count);
			Assert.AreEqual(2, graph.Nodes[2].Transitions.Count);
			Assert.AreEqual(1, graph.Nodes[3].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[4].Transitions.Count);
			Assert.AreEqual(0, graph.Nodes[5].Transitions.Count);
			Assert.IsTrue(graph.Nodes[0].Transitions[0].Input.Match('a'));
			Assert.IsTrue(graph.Nodes[1].Transitions[0].Input.Match('b'));
			Assert.IsTrue(graph.Nodes[1].Transitions[1].Input.Match('z'));
			Assert.IsTrue(graph.Nodes[2].Transitions[0].Input.Match('c'));
			Assert.IsTrue(graph.Nodes[2].Transitions[1].Input.Match('d'));
			Assert.IsTrue(graph.Nodes[3].Transitions[0].Input.Match('d'));

			for (int t = 0; t < 4; t++) Assert.AreEqual(0, graph.Nodes[t].RecognizedRules.Count);

			Assert.AreEqual(1, graph.Nodes[5].RecognizedRules.Count);
			Assert.AreEqual(1, graph.Nodes[4].RecognizedRules.Count);
		}


	}


}
