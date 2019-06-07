using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;
using System.Collections.Generic;
using System.Linq;

namespace FSMLib.UnitTest.Situations
{
	[TestClass]
	public class SituationGraphSegmentFactoryUnitTest
	{
		
		[TestMethod]
		public void ShouldBuildSituationPredicateSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a");
			segment=factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate, capEdge.AsEnumerable());
			Assert.AreEqual(1, nodes.Count);
			Assert.AreEqual(1, segment.InputEdges.Count());
			Assert.AreEqual(1, segment.OutputNodes.Count());
			Assert.AreEqual(1, segment.OutputNodes.ElementAt(0).Edges.Count);
			Assert.IsTrue(segment.InputEdges.ElementAt(0).Predicate.Match('a'));

			Assert.AreEqual(capEdge, segment.OutputNodes.ElementAt(0).Edges.ElementAt(0));
		}
		
		[TestMethod]
		public void ShouldBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=abc");
			segment = factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate , capEdge.AsEnumerable());
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
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a|b|c");
			segment = factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate , capEdge.AsEnumerable());
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
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a?");
			segment = factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate, capEdge.AsEnumerable());
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
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a*");
			segment = factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate, capEdge.AsEnumerable());
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
			Rule<char> rule;
			SituationEdge<char> capEdge;
			SituationGraphSegment<char> segment;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			rule = RuleHelper.BuildRule("A=a+");
			segment = factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), rule.Predicate, capEdge.AsEnumerable());
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
			Rule<char> rule;
			SituationEdge<char> capEdge;
			Terminal<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Terminal<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as Terminal<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}

		[TestMethod]
		public void ShouldNotBuildSequenceSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			Sequence<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Sequence<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as Sequence<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOrSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			Or<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Or<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as Or<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOptionalSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			Optional<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new Optional<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as Optional<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildZeroOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			ZeroOrMore<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new ZeroOrMore<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as ZeroOrMore<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}
		[TestMethod]
		public void ShouldNotBuildOneOrMoreSegment()
		{
			SituationGraphSegmentFactory<char> factory;
			List<SituationNode<char>> nodes;
			Rule<char> rule;
			SituationEdge<char> capEdge;
			OneOrMore<char> predicate;

			capEdge = new SituationEdge<char>();
			nodes = new List<SituationNode<char>>();
			factory = new SituationGraphSegmentFactory<char>();

			predicate = new OneOrMore<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, rule, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, null, Enumerable.Empty<char>(), predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, null, predicate, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), null as OneOrMore<char>, capEdge.AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(nodes, rule, Enumerable.Empty<char>(), predicate, null));
		}



	}
}
