﻿using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
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

namespace FSMLib.UnitTest.ActionTables
{
	[TestClass]
	public class ActionTableFactoryContextUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableFactoryContext<char>(null, new ActionTable<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableFactoryContext<char>(new MockedSegmentFactoryProvider<char>(), null));
		}

		[TestMethod]
		public void ShouldBuildSegmentFromBasicSequence()
		{
			Segment<char> segment;
			Rule<char> rule;
			Sequence<char> predicate;
			ActionTableFactoryContext<char> context;


			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());

			segment = context.BuildSegment(rule, Enumerable.Empty<BaseAction<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
		}
		[TestMethod]
		public void ShouldNotThrowExceptionWhenRulesAreRecursive()
		{
			Rule<char> rule1, rule2;
			NonTerminal<char> predicate1, predicate2;
			ActionTableFactoryContext<char> context;
			Segment<char> segment;

			predicate1 = new NonTerminal<char>() { Name = "B" };
			predicate2 = new NonTerminal<char>() { Name = "A" };
			rule1 = new Rule<char>() { Name = "A" };
			rule1.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'a' }, predicate1, new Terminal<char>() { Value = 'a' } };
			rule2 = new Rule<char>() { Name = "B" };
			rule2.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'b' }, predicate2, new Terminal<char>() { Value = 'b' } };

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());

			segment = context.BuildSegment(rule1, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			segment = context.BuildSegment(rule2, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);

		}

		[TestMethod]
		public void ShouldUseCacheForSegments()
		{
			Segment<char> segment1, segment2;
			Rule<char> rule;
			Sequence<char> predicate;
			ActionTableFactoryContext<char> context;

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());

			segment1 = context.BuildSegment(rule, Enumerable.Empty<Reduce<char>>());
			segment2 = context.BuildSegment(rule, Enumerable.Empty<Reduce<char>>());
			Assert.AreEqual(segment1, segment2);
		}
		[TestMethod]
		public void ShouldReturnTargetState()
		{
			ActionTableFactoryContext<char> context;
			State<char> target;
			ActionTable<char> actionTable;


			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			actionTable.States.Add(new State<char>());
			actionTable.States.Add(new State<char>());

			target = context.GetTargetState(1);
			Assert.AreEqual(actionTable.States[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetState()
		{
			ActionTableFactoryContext<char> context;
			ActionTable<char> actionTable;

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			actionTable.States.Add(new State<char>());
			actionTable.States.Add(new State<char>());

			Assert.ThrowsException<IndexOutOfRangeException>(() => context.GetTargetState(2));
		}
		[TestMethod]
		public void ShouldReturnStateIndex()
		{
			ActionTableFactoryContext<char> context;
			State<char> a, b;

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());
			a = context.CreateState();
			b = context.CreateState();

			Assert.AreEqual(0, context.GetStateIndex(a));
			Assert.AreEqual(1, context.GetStateIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfStateDoesntExists()
		{
			ActionTableFactoryContext<char> context;

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());

			Assert.AreEqual(-1, context.GetStateIndex(new State<char>()));
		}

		[TestMethod]
		public void ShouldCreateState()
		{
			ActionTableFactoryContext<char> context;
			State<char> a, b;
			ActionTable<char> actionTable;

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);

			a = context.CreateState();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, actionTable.States.Count);
			b = context.CreateState();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, actionTable.States.Count);
		}

		[TestMethod]
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			ActionTableFactoryContext<char> context;

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new ActionTable<char>());

			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(null, Enumerable.Empty<BaseAction<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(Enumerable.Empty<State<char>>(), null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			ActionTable<char> actionTable;
			State<char> a, b;
			ShiftOnTerminal<char> action;
			ActionTableFactoryContext<char> context;

			actionTable = new ActionTable<char>();
			a = new State<char>(); actionTable.States.Add(a);
			b = new State<char>(); actionTable.States.Add(b);

			action = new ShiftOnTerminal<char>() { Value = 'a', TargetStateIndex = actionTable.States.IndexOf(b) };

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			context.Connect(a.AsEnumerable(), action.AsEnumerable());

			Assert.AreEqual(1, a.TerminalActions.Count);
			Assert.AreEqual(0, b.TerminalActions.Count);
			Assert.AreEqual(1, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual('a', a.TerminalActions[0].Value);
		}



		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			State<char> a, b, c;
			ShiftOnTerminal<char> actionToB, actionToC;

			actionTable = new ActionTable<char>();
			a = new State<char>(); actionTable.States.Add(a);
			b = new State<char>(); actionTable.States.Add(b);
			c = new State<char>(); actionTable.States.Add(c);


			actionToB = new ShiftOnTerminal<char>() { Value = 'a', TargetStateIndex = actionTable.States.IndexOf(b) };
			actionToC = new ShiftOnTerminal<char>() { Value = 'b', TargetStateIndex = actionTable.States.IndexOf(c) };

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			context.Connect(a.AsEnumerable(), new ShiftOnTerminal<char>[] { actionToB, actionToC });

			Assert.AreEqual(2, a.TerminalActions.Count);
			Assert.AreEqual(0, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(1, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, a.TerminalActions[1].TargetStateIndex);
			Assert.AreEqual('a', a.TerminalActions[0].Value);
			Assert.AreEqual('b', a.TerminalActions[1].Value);
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			State<char> a, b, c;
			ShiftOnTerminal<char> action;

			actionTable = new ActionTable<char>();
			a = new State<char>(); actionTable.States.Add(a);
			b = new State<char>(); actionTable.States.Add(b);
			c = new State<char>(); actionTable.States.Add(c);

			action = new ShiftOnTerminal<char>() { Value = 'a', TargetStateIndex = actionTable.States.IndexOf(c) };

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			context.Connect(new State<char>[] { a, b }, action.AsEnumerable());

			Assert.AreEqual(1, a.TerminalActions.Count);
			Assert.AreEqual(1, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(2, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, b.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual('a', a.TerminalActions[0].Value);
			Assert.AreEqual('a', b.TerminalActions[0].Value);
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			State<char> a, b, c, d;
			ShiftOnTerminal<char> actionToD, actionToC;

			actionTable = new ActionTable<char>();
			a = new State<char>(); actionTable.States.Add(a);
			b = new State<char>(); actionTable.States.Add(b);
			c = new State<char>(); actionTable.States.Add(c);
			d = new State<char>(); actionTable.States.Add(d);

			actionToC = new ShiftOnTerminal<char>() { Value = 'a', TargetStateIndex = actionTable.States.IndexOf(c) };
			actionToD = new ShiftOnTerminal<char>() { Value = 'b', TargetStateIndex = actionTable.States.IndexOf(d) };

			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			context.Connect(new State<char>[] { a, b }, new ShiftOnTerminal<char>[] { actionToC, actionToD });

			Assert.AreEqual(2, a.TerminalActions.Count);
			Assert.AreEqual(2, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(0, d.TerminalActions.Count);
			Assert.AreEqual(2, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, b.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(3, a.TerminalActions[1].TargetStateIndex);
			Assert.AreEqual(3, b.TerminalActions[1].TargetStateIndex);

			Assert.AreEqual('a', a.TerminalActions[0].Value);
			Assert.AreEqual('a', b.TerminalActions[0].Value);
			Assert.AreEqual('b', a.TerminalActions[1].Value);
			Assert.AreEqual('b', b.TerminalActions[1].Value);
		}


		[TestMethod]
		public void ShouldGetFirstTerminalForRule()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items=context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetDistinctFirstTerminalForRule()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=aef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual('a', items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=def");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetLoopedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A={B}bc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("B=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetFirstTerminalsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual('a', items[0]);
			Assert.AreEqual('d', items[1]);
		}

		[TestMethod]
		public void ShouldGetRuleReductionDependency()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual("A", items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedRuleReductionDependency()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}

		[TestMethod]
		public void ShouldGetLoopedRuleReductionDependency()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}


		[TestMethod]
		public void ShouldGetFirstTerminalsAfterAction()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			char[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=a{B}c");
			

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			context.BuildSegment(rules[0], Enumerable.Empty<BaseAction<char>>());

			items = context.GetFirstTerminalsAfterAction(actionTable.States[2], "B").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual('c', items[0]);
		}

		


		public void ShouldGetStateWithReductionRule()
		{
			Rule<char>[] rules;
			ActionTable<char> actionTable;
			ActionTableFactoryContext<char> context;
			Reduce<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			actionTable = new ActionTable<char>();
			context = new ActionTableFactoryContext<char>(new SegmentFactoryProvider<char>(), actionTable);
			items = context.GetReductionActions("A").ToArray();
			Assert.AreEqual(2, items.Length);
		}



	}

}