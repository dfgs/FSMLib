using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Graphs
{
	[TestClass]
	public class GraphFactoryContextUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactoryContext<char>(null, new Graph<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactoryContext<char>(new MockedSegmentFactoryProvider<char>(), null));
		}

		[TestMethod]
		public void ShouldBuildSegmentFromBasicSequence()
		{
			Segment<char> segment;
			Rule<char> rule;
			Sequence<char> predicate;
			GraphFactoryContext<char> context;


			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			segment = context.BuildSegment(rule, Enumerable.Empty<BaseTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
		}
		[TestMethod]
		public void ShouldNotThrowExceptionWhenRulesAreRecursive()
		{
			Rule<char> rule1, rule2;
			NonTerminal<char> predicate1, predicate2;
			GraphFactoryContext<char> context;
			Segment<char> segment;

			predicate1 = new NonTerminal<char>() { Name = "B" };
			predicate2 = new NonTerminal<char>() { Name = "A" };
			rule1 = new Rule<char>() { Name = "A" };
			rule1.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'a' }, predicate1, new Terminal<char>() { Value = 'a' } };
			rule2 = new Rule<char>() { Name = "B" };
			rule2.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'b' }, predicate2, new Terminal<char>() { Value = 'b' } };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			segment = context.BuildSegment(rule1, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			segment = context.BuildSegment(rule2, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);

		}

		[TestMethod]
		public void ShouldUseCacheForSegments()
		{
			Segment<char> segment1, segment2;
			Rule<char> rule;
			Sequence<char> predicate;
			GraphFactoryContext<char> context;

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			segment1 = context.BuildSegment(rule, Enumerable.Empty<ReductionTransition<char>>());
			segment2 = context.BuildSegment(rule, Enumerable.Empty<ReductionTransition<char>>());
			Assert.AreEqual(segment1, segment2);
		}
		[TestMethod]
		public void ShouldReturnTargetNode()
		{
			GraphFactoryContext<char> context;
			Node<char> target;
			Graph<char> graph;


			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			target = context.GetTargetNode(1);
			Assert.AreEqual(graph.Nodes[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetNode()
		{
			GraphFactoryContext<char> context;
			Graph<char> graph;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			Assert.ThrowsException<IndexOutOfRangeException>(() => context.GetTargetNode(2));
		}
		[TestMethod]
		public void ShouldReturnNodeIndex()
		{
			GraphFactoryContext<char> context;
			Node<char> a, b;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());
			a = context.CreateNode();
			b = context.CreateNode();

			Assert.AreEqual(0, context.GetNodeIndex(a));
			Assert.AreEqual(1, context.GetNodeIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfNodeDoesntExists()
		{
			GraphFactoryContext<char> context;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			Assert.AreEqual(-1, context.GetNodeIndex(new Node<char>()));
		}

		[TestMethod]
		public void ShouldCreateNode()
		{
			GraphFactoryContext<char> context;
			Node<char> a, b;
			Graph<char> graph;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);

			a = context.CreateNode();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, graph.Nodes.Count);
			b = context.CreateNode();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, graph.Nodes.Count);
		}

		[TestMethod]
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			GraphFactoryContext<char> context;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(null, Enumerable.Empty<BaseTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(Enumerable.Empty<Node<char>>(), null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			Graph<char> graph;
			Node<char> a, b;
			TerminalTransition<char> transition;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);

			transition = new TerminalTransition<char>() { Value = 'a', TargetNodeIndex = graph.Nodes.IndexOf(b) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(a.AsEnumerable(), transition.AsEnumerable());

			Assert.AreEqual(1, a.TerminalTransitions.Count);
			Assert.AreEqual(0, b.TerminalTransitions.Count);
			Assert.AreEqual(1, a.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual('a', a.TerminalTransitions[0].Value);
		}



		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			Graph<char> graph;
			GraphFactoryContext<char> context;
			Node<char> a, b, c;
			TerminalTransition<char> transitionToB, transitionToC;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);


			transitionToB = new TerminalTransition<char>() { Value = 'a', TargetNodeIndex = graph.Nodes.IndexOf(b) };
			transitionToC = new TerminalTransition<char>() { Value = 'b', TargetNodeIndex = graph.Nodes.IndexOf(c) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(a.AsEnumerable(), new TerminalTransition<char>[] { transitionToB, transitionToC });

			Assert.AreEqual(2, a.TerminalTransitions.Count);
			Assert.AreEqual(0, b.TerminalTransitions.Count);
			Assert.AreEqual(0, c.TerminalTransitions.Count);
			Assert.AreEqual(1, a.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual(2, a.TerminalTransitions[1].TargetNodeIndex);
			Assert.AreEqual('a', a.TerminalTransitions[0].Value);
			Assert.AreEqual('b', a.TerminalTransitions[1].Value);
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			Graph<char> graph;
			GraphFactoryContext<char> context;
			Node<char> a, b, c;
			TerminalTransition<char> transition;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);

			transition = new TerminalTransition<char>() { Value = 'a', TargetNodeIndex = graph.Nodes.IndexOf(c) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(new Node<char>[] { a, b }, transition.AsEnumerable());

			Assert.AreEqual(1, a.TerminalTransitions.Count);
			Assert.AreEqual(1, b.TerminalTransitions.Count);
			Assert.AreEqual(0, c.TerminalTransitions.Count);
			Assert.AreEqual(2, a.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual('a', a.TerminalTransitions[0].Value);
			Assert.AreEqual('a', b.TerminalTransitions[0].Value);
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			Graph<char> graph;
			GraphFactoryContext<char> context;
			Node<char> a, b, c, d;
			TerminalTransition<char> transitionToD, transitionToC;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			d = new Node<char>(); graph.Nodes.Add(d);

			transitionToC = new TerminalTransition<char>() { Value = 'a', TargetNodeIndex = graph.Nodes.IndexOf(c) };
			transitionToD = new TerminalTransition<char>() { Value = 'b', TargetNodeIndex = graph.Nodes.IndexOf(d) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(new Node<char>[] { a, b }, new TerminalTransition<char>[] { transitionToC, transitionToD });

			Assert.AreEqual(2, a.TerminalTransitions.Count);
			Assert.AreEqual(2, b.TerminalTransitions.Count);
			Assert.AreEqual(0, c.TerminalTransitions.Count);
			Assert.AreEqual(0, d.TerminalTransitions.Count);
			Assert.AreEqual(2, a.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.TerminalTransitions[0].TargetNodeIndex);
			Assert.AreEqual(3, a.TerminalTransitions[1].TargetNodeIndex);
			Assert.AreEqual(3, b.TerminalTransitions[1].TargetNodeIndex);

			Assert.AreEqual('a', a.TerminalTransitions[0].Value);
			Assert.AreEqual('a', b.TerminalTransitions[0].Value);
			Assert.AreEqual('b', a.TerminalTransitions[1].Value);
			Assert.AreEqual('b', b.TerminalTransitions[1].Value);
		}


		[TestMethod]
		public void ShouldGetFirstTerminalForRule()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items=context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetDistinctFirstTerminalForRule()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=aef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual('a', items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=def");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetLoopedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A={B}bc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("B=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetRuleReductionDependency()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual("A", items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedRuleReductionDependency()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}

		[TestMethod]
		public void ShouldGetLoopedRuleReductionDependency()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}


		[TestMethod]
		public void ShouldGetFirstTerminalsAfterTransition()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=a{B}c");
			

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.BuildSegment(rules[0], Enumerable.Empty<BaseTransition<char>>());

			items = context.GetFirstTerminalsAfterTransition(rules, graph.Nodes[2].NonTerminalTransitions[0]).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual('c', items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedFirstTerminalsAfterTransition()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[2];
			rules[0] = RuleHelper.BuildRule("A=a{B}{C}");
			rules[1] = RuleHelper.BuildRule("C=abc");


			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.BuildSegment(rules[0], Enumerable.Empty<BaseTransition<char>>());
			context.BuildSegment(rules[1], Enumerable.Empty<BaseTransition<char>>());

			items = context.GetFirstTerminalsAfterTransition(rules, graph.Nodes[2].NonTerminalTransitions[0]).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual('a', items[0]);
		}


		public void ShouldGetNodeWithReductionRule()
		{
			Rule<char>[] rules;
			Graph<char> graph;
			GraphFactoryContext<char> context;
			ReductionTransition<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			items = context.GetReductionTransitions("A").ToArray();
			Assert.AreEqual(2, items.Length);
		}



	}

}