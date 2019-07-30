using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;
using System.Collections.Generic;
using System.Linq;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;

namespace FSMLib.LexicalAnalysys.UnitTest.Situations
{
	[TestClass]
	public class SituationGraphSegmentFactoryUnitTest
	{
		
		[TestMethod]
		public void ShouldBuildSituationPredicateSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a");
			segment=factory.BuildSegment(nodes, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildAnySegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=.");
			segment = factory.BuildSegment(nodes, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('c'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildTerminalRange()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=[a-c]");
			segment = factory.BuildSegment(nodes, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('c'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=abc");
			segment = factory.BuildSegment(nodes, rule, rule.Predicate , capEdge.AsEnumerable());
			Assert.AreEqual(3, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));

			Assert.AreEqual(1, nodes[0].Edges.Count);
			Assert.AreEqual(1, nodes[1].Edges.Count);
			Assert.AreEqual(1, nodes[2].Edges.Count);

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}

		[TestMethod]
		public void ShouldBuildOrSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a|b|c");
			segment = factory.BuildSegment(nodes, rule,  rule.Predicate , capEdge.AsEnumerable());
			Assert.AreEqual(3, nodes.Count);
			Assert.AreEqual(3, segment.InputEdges.Count());
			Assert.AreEqual(3, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(1).Edges.Count);
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(2).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.IsTrue(segment.InputEdges.ElementAt(1).Predicate.Match('b'));
			Assert.IsTrue(segment.InputEdges.ElementAt(2).Predicate.Match('c'));

			Assert.AreEqual(1, nodes[0].Edges.Count);
			Assert.AreEqual(1, nodes[1].Edges.Count);
			Assert.AreEqual(1, nodes[2].Edges.Count);

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(1).Edges.ElementAt(0));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(2).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildOptionalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a?");
			segment = factory.BuildSegment(nodes, rule,  rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(2, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.AreEqual(capEdge, segment.InputEdges.ElementAt(1));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildZeroOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a*");
			segment = factory.BuildSegment(nodes, rule, rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(2, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(2, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.AreEqual(capEdge, segment.InputEdges.ElementAt(1));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		[TestMethod]
		public void ShouldBuildOneOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a+");
			segment = factory.BuildSegment(nodes, rule,  rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(2, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));
			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}





		[TestMethod]
		public void ShouldNotBuildSituationPredicateSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			Letter predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Letter('a');
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as Letter, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}

		[TestMethod]
		public void ShouldNotBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			Sequence predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Sequence();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as Sequence, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOrSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			Or predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Or();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as Or, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOptionalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			Optional predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Optional();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as Optional, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildZeroOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			ZeroOrMore predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new ZeroOrMore();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as ZeroOrMore, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}

		[TestMethod]
		public void ShouldNotBuildAnyTerminalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			AnyLetter predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new AnyLetter();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes,  null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as AnyLetter, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildTerminalRangeSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			LettersRange predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new LettersRange('a','b');
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as LettersRange, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOneOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			LexicalRule rule;
			SituationEdge<char> capEdge;
			OneOrMore predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new OneOrMore();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule,  predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  null as OneOrMore, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule,  predicate, null));
		}



	}
}
