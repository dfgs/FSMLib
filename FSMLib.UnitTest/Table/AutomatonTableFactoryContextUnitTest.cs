using FSMLib.Table;
using FSMLib.Actions;
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
using FSMLib.Inputs;

namespace FSMLib.UnitTest.AutomatonTables
{
	[TestClass]
	public class AutomatonTableFactoryContextUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableFactoryContext<char>(null, new AutomatonTable<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableFactoryContext<char>(new MockedSegmentFactoryProvider<char>(), null));
		}

		[TestMethod]
		public void ShouldBuildSegmentFromBasicSequence()
		{
			Segment<char> segment;
			Rule<char> rule;
			Sequence<char> predicate;
			AutomatonTableFactoryContext<char> context;


			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());

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
			AutomatonTableFactoryContext<char> context;
			Segment<char> segment;

			predicate1 = new NonTerminal<char>() { Name = "B" };
			predicate2 = new NonTerminal<char>() { Name = "A" };
			rule1 = new Rule<char>() { Name = "A" };
			rule1.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'a' }, predicate1, new Terminal<char>() { Value = 'a' } };
			rule2 = new Rule<char>() { Name = "B" };
			rule2.Predicate = (Sequence<char>)new BasePredicate<char>[] { new Terminal<char>() { Value = 'b' }, predicate2, new Terminal<char>() { Value = 'b' } };

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());

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
			AutomatonTableFactoryContext<char> context;

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());

			segment1 = context.BuildSegment(rule, Enumerable.Empty<Reduce<char>>());
			segment2 = context.BuildSegment(rule, Enumerable.Empty<Reduce<char>>());
			Assert.AreEqual(segment1, segment2);
		}
		[TestMethod]
		public void ShouldReturnTargetState()
		{
			AutomatonTableFactoryContext<char> context;
			State<char> target;
			AutomatonTable<char> automatonTable;


			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			automatonTable.States.Add(new State<char>());
			automatonTable.States.Add(new State<char>());

			target = context.GetTargetState(1);
			Assert.AreEqual(automatonTable.States[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetState()
		{
			AutomatonTableFactoryContext<char> context;
			AutomatonTable<char> automatonTable;

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			automatonTable.States.Add(new State<char>());
			automatonTable.States.Add(new State<char>());

			Assert.ThrowsException<IndexOutOfRangeException>(() => context.GetTargetState(2));
		}
		[TestMethod]
		public void ShouldReturnStateIndex()
		{
			AutomatonTableFactoryContext<char> context;
			State<char> a, b;

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());
			a = context.CreateState();
			b = context.CreateState();

			Assert.AreEqual(0, context.GetStateIndex(a));
			Assert.AreEqual(1, context.GetStateIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfStateDoesntExists()
		{
			AutomatonTableFactoryContext<char> context;

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());

			Assert.AreEqual(-1, context.GetStateIndex(new State<char>()));
		}

		[TestMethod]
		public void ShouldCreateState()
		{
			AutomatonTableFactoryContext<char> context;
			State<char> a, b;
			AutomatonTable<char> automatonTable;

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);

			a = context.CreateState();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, automatonTable.States.Count);
			b = context.CreateState();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, automatonTable.States.Count);
		}

		[TestMethod]
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			AutomatonTableFactoryContext<char> context;

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), new AutomatonTable<char>());

			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(null, Enumerable.Empty<BaseAction<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(Enumerable.Empty<State<char>>(), null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			AutomatonTable<char> automatonTable;
			State<char> a, b;
			ShiftOnTerminal<char> action;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			a = new State<char>(); automatonTable.States.Add(a);
			b = new State<char>(); automatonTable.States.Add(b);

			action = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = automatonTable.States.IndexOf(b) };

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			context.Connect(a.AsEnumerable(), action.AsEnumerable());

			Assert.AreEqual(1, a.TerminalActions.Count);
			Assert.AreEqual(0, b.TerminalActions.Count);
			Assert.AreEqual(1, a.TerminalActions[0].TargetStateIndex);
			Assert.IsTrue(a.TerminalActions[0].Input.Match('a'));
		}



		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			State<char> a, b, c;
			ShiftOnTerminal<char> actionToB, actionToC;

			automatonTable = new AutomatonTable<char>();
			a = new State<char>(); automatonTable.States.Add(a);
			b = new State<char>(); automatonTable.States.Add(b);
			c = new State<char>(); automatonTable.States.Add(c);


			actionToB = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = automatonTable.States.IndexOf(b) };
			actionToC = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'b' }, TargetStateIndex = automatonTable.States.IndexOf(c) };

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			context.Connect(a.AsEnumerable(), new ShiftOnTerminal<char>[] { actionToB, actionToC });

			Assert.AreEqual(2, a.TerminalActions.Count);
			Assert.AreEqual(0, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(1, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, a.TerminalActions[1].TargetStateIndex);
			Assert.IsTrue(a.TerminalActions[0].Input.Match('a'));
			Assert.IsTrue(a.TerminalActions[1].Input.Match('b'));
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			State<char> a, b, c;
			ShiftOnTerminal<char> action;

			automatonTable = new AutomatonTable<char>();
			a = new State<char>(); automatonTable.States.Add(a);
			b = new State<char>(); automatonTable.States.Add(b);
			c = new State<char>(); automatonTable.States.Add(c);

			action = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = automatonTable.States.IndexOf(c) };

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			context.Connect(new State<char>[] { a, b }, action.AsEnumerable());

			Assert.AreEqual(1, a.TerminalActions.Count);
			Assert.AreEqual(1, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(2, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, b.TerminalActions[0].TargetStateIndex);
			Assert.IsTrue(a.TerminalActions[0].Input.Match('a'));
			Assert.IsTrue(b.TerminalActions[0].Input.Match('a'));
			
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			State<char> a, b, c, d;
			ShiftOnTerminal<char> actionToD, actionToC;

			automatonTable = new AutomatonTable<char>();
			a = new State<char>(); automatonTable.States.Add(a);
			b = new State<char>(); automatonTable.States.Add(b);
			c = new State<char>(); automatonTable.States.Add(c);
			d = new State<char>(); automatonTable.States.Add(d);

			actionToC = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = automatonTable.States.IndexOf(c) };
			actionToD = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'b' }, TargetStateIndex = automatonTable.States.IndexOf(d) };

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			context.Connect(new State<char>[] { a, b }, new ShiftOnTerminal<char>[] { actionToC, actionToD });

			Assert.AreEqual(2, a.TerminalActions.Count);
			Assert.AreEqual(2, b.TerminalActions.Count);
			Assert.AreEqual(0, c.TerminalActions.Count);
			Assert.AreEqual(0, d.TerminalActions.Count);
			Assert.AreEqual(2, a.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(2, b.TerminalActions[0].TargetStateIndex);
			Assert.AreEqual(3, a.TerminalActions[1].TargetStateIndex);
			Assert.AreEqual(3, b.TerminalActions[1].TargetStateIndex);

			Assert.IsTrue(a.TerminalActions[0].Input.Match('a'));
			Assert.IsTrue(b.TerminalActions[0].Input.Match('a'));
			Assert.IsTrue(a.TerminalActions[1].Input.Match('b'));
			Assert.IsTrue(b.TerminalActions[1].Input.Match('b'));
		}

		[TestMethod]
		public void ShouldNotConnectIfAlreadyConnected()
		{
			AutomatonTable<char> automatonTable;
			State<char> a, b;
			ShiftOnTerminal<char> shiftTerminal;
			ShiftOnNonTerminal<char> shiftNonTerminal;
			Reduce<char> reduce;
			//Accept<char> accept;

			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			a = new State<char>(); automatonTable.States.Add(a);
			b = new State<char>(); automatonTable.States.Add(b);

			shiftTerminal = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = automatonTable.States.IndexOf(b) };
			shiftNonTerminal = new ShiftOnNonTerminal<char>() { Name = "a", TargetStateIndex = automatonTable.States.IndexOf(b) };
			reduce = new Reduce<char>() { Name = "A" };
			//accept = new Accept<char>();

			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);

			context.Connect(a.AsEnumerable(), shiftTerminal.AsEnumerable());
			Assert.AreEqual(1, a.TerminalActions.Count);
			context.Connect(a.AsEnumerable(), shiftTerminal.AsEnumerable());
			Assert.AreEqual(1, a.TerminalActions.Count);

			context.Connect(a.AsEnumerable(), shiftNonTerminal.AsEnumerable());
			Assert.AreEqual(1, a.NonTerminalActions.Count);
			context.Connect(a.AsEnumerable(), shiftNonTerminal.AsEnumerable());
			Assert.AreEqual(1, a.NonTerminalActions.Count);

			context.Connect(a.AsEnumerable(), reduce.AsEnumerable());
			Assert.AreEqual(1, a.ReductionActions.Count);
			context.Connect(a.AsEnumerable(), reduce.AsEnumerable());
			Assert.AreEqual(1, a.ReductionActions.Count);

			/*context.Connect(a.AsEnumerable(), accept.AsEnumerable());
			Assert.AreEqual(1, a.AcceptActions.Count);
			context.Connect(a.AsEnumerable(), accept.AsEnumerable());
			Assert.AreEqual(1, a.AcceptActions.Count);*/



		}

		[TestMethod]
		public void ShouldGetFirstTerminalForRule()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			BaseTerminalInput<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items=context.GetFirstTerminalInputsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue( items[0].Match('a'));
			Assert.IsTrue( items[1].Match('d'));
		}

		[TestMethod]
		public void ShouldGetDistinctFirstTerminalForRule()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			BaseTerminalInput<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=aef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetFirstTerminalInputsForRule(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Match('a'));
		}

		[TestMethod]
		public void ShouldGetNestedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			BaseTerminalInput<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=def");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetFirstTerminalInputsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items[0].Match('a'));
			Assert.IsTrue(items[1].Match('d'));
		}

		[TestMethod]
		public void ShouldGetLoopedFirstTerminalForRule()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			BaseTerminalInput<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A={B}bc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("B=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetFirstTerminalInputsForRule(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items[0].Match('a'));
			Assert.IsTrue(items[1].Match('d'));
		}

		[TestMethod]
		public void ShouldGetRuleReductionDependency()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual("A", items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedRuleReductionDependency()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}

		[TestMethod]
		public void ShouldGetLoopedRuleReductionDependency()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			string[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A={B}ef");
			rules[2] = RuleHelper.BuildRule("B={A}bc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetRuleReductionDependency(rules, "A").ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual("A", items[0]);
			Assert.AreEqual("B", items[1]);

		}


		[TestMethod]
		public void ShouldGetFirstTerminalsAfterAction()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			BaseTerminalInput<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=a{B}c");
			

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			context.BuildSegment(rules[0], Enumerable.Empty<BaseAction<char>>());

			items = context.GetFirstTerminalInputsAfterAction( automatonTable.States[2].NonTerminalActions.First()).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Match('c'));
		}




		public void ShouldGetStateWithReductionRule()
		{
			Rule<char>[] rules;
			AutomatonTable<char> automatonTable;
			AutomatonTableFactoryContext<char> context;
			Reduce<char>[] items;

			rules = new Rule<char>[4];
			rules[0] = RuleHelper.BuildRule("A=abc");
			rules[1] = RuleHelper.BuildRule("A=def");
			rules[2] = RuleHelper.BuildRule("B=abc");
			rules[3] = RuleHelper.BuildRule("C=abc");

			automatonTable = new AutomatonTable<char>();
			context = new AutomatonTableFactoryContext<char>(new SegmentFactoryProvider<char>(), automatonTable);
			items = context.GetReductionActions("A").ToArray();
			Assert.AreEqual(2, items.Length);
		}



	}

}