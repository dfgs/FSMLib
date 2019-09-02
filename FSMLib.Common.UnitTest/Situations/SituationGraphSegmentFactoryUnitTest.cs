using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Predicates;
using FSMLib.Situations;
using System.Collections.Generic;
using System.Linq;
using FSMLib.Common.Situations;
using FSMLib.Common;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Situations
{
	[TestClass]
	public class SituationGraphSegmentFactoryUnitTest
	{
		
		[TestMethod]
		public void ShouldBuildSituationPredicateSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new Terminal('a');
			segment = factory.BuildSegment(graph, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildAnySegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new AnyTerminal();
			segment = factory.BuildSegment(graph, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('c'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildTerminalRange()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new TerminalsRange('a',  'c');
			segment = factory.BuildSegment(graph, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('c'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new Sequence(new Terminal('a'), new Terminal('b'), new Terminal('c'));
			segment = factory.BuildSegment(graph, rule, rule.Predicate , capEdge.AsEnumerable());
			Assert.AreEqual(3, graph.Nodes.Count());
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));

			Assert.AreEqual(1, graph.Nodes.ElementAt(0).Edges.Count());
			Assert.AreEqual(1, graph.Nodes.ElementAt(1).Edges.Count());
			Assert.AreEqual(1, graph.Nodes.ElementAt(2).Edges.Count());

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}

		[TestMethod]
		public void ShouldBuildOrSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new Or(new Terminal('a'), new Terminal('b'), new Terminal('c'));
			segment = factory.BuildSegment(graph, rule,  rule.Predicate , capEdge.AsEnumerable());
			Assert.AreEqual(3, graph.Nodes.Count());
			Assert.AreEqual(3, segment.InputEdges.Count());
			Assert.AreEqual(3, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(1).Edges.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(2).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(1).Predicate.GetInputs().First().Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(2).Predicate.GetInputs().First().Match('c'));

			Assert.AreEqual(1, graph.Nodes.ElementAt(0).Edges.Count());
			Assert.AreEqual(1, graph.Nodes.ElementAt(1).Edges.Count());
			Assert.AreEqual(1, graph.Nodes.ElementAt(2).Edges.Count());

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(1).Edges.ElementAt(0));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(2).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildOptionalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new Optional() { Item = new Terminal('a') };
			segment = factory.BuildSegment(graph, rule,  rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(2, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.AreEqual(capEdge, segment.InputEdges.ElementAt(1));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildZeroOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new ZeroOrMore() { Item = new Terminal('a') };
			segment = factory.BuildSegment(graph, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(2, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(2, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.AreEqual(capEdge, segment.InputEdges.ElementAt(1));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildOneOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ISituationGraphSegment<char> segment;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = new LexicalRule();
			rule.Predicate = new OneOrMore() { Item=new Terminal('a') };
			segment = factory.BuildSegment(graph, rule,  rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, graph.Nodes.Count());
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(2, segment.OutputNodes.ElementAt(0).Edges.Count());
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.GetInputs().First().Match('a'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}





		[TestMethod]
		public void ShouldNotBuildSituationPredicateSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			Terminal predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Terminal('a');
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as Terminal, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}

		[TestMethod]
		public void ShouldNotBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			Sequence predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Sequence();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as Sequence, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOrSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			Or predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Or();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as Or, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOptionalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			Optional predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Optional();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as Optional, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildZeroOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			ZeroOrMore predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new ZeroOrMore();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as ZeroOrMore, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}

		[TestMethod]
		public void ShouldNotBuildAnyTerminalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			AnyTerminal predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new AnyTerminal();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as AnyTerminal, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildTerminalRangeSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			TerminalsRange predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new TerminalsRange('a','b');
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as TerminalsRange, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOneOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			ISituationGraph<char> graph;
			LexicalRule rule;
			MockedSituationEdge capEdge;
			OneOrMore predicate;

			capEdge = new MockedSituationEdge();
			graph = new SituationGraph<char>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new OneOrMore();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  null as OneOrMore, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(graph, rule,  predicate, null));
		}



	}
}
