﻿using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.Rules;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.Situations;
using FSMLib.Actions;
using FSMLib.Predicates;

namespace FSMLib.UnitTest.Situations
{
	[TestClass]
	public class SituationProducerUnitTest
	{

		[TestMethod]
		public void ShouldNotGetDistinctTerminal()
		{
			SituationProducer<char> producer;

			producer = new SituationProducer<char>();
			Assert.ThrowsException<ArgumentNullException>(() => producer.GetNextTerminalInputs(null) );
		}


		[TestMethod]
		public void ShouldGetOneDistinctTerminal()
		{
			Situation2<char> s1,s2,s3;
			SituationProducer<char> producer;
			BaseTerminalInput<char>[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A=a|a|a");

			s1 = new Situation2<char>() { Rule=rule, Predicate= (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs=producer.GetNextTerminalInputs(new Situation2<char>[] { s1,s2,s3 }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
		}

		[TestMethod]
		public void ShouldGetTwoDistinctTerminals()
		{
			Situation2<char> s1, s2, s3;
			SituationProducer<char> producer;
			BaseTerminalInput<char>[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A=a|a|b");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminalInputs(new Situation2<char>[] { s1, s2, s3 }).ToArray();


			Assert.AreEqual(2, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));

		}

		[TestMethod]
		public void ShouldGetThreeDistinctTerminals()
		{
			Situation2<char> s1, s2, s3;
			SituationProducer<char> producer;
			BaseTerminalInput<char>[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A=a|b|c");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminalInputs(new Situation2<char>[] { s1, s2, s3 }).ToArray();


			Assert.AreEqual(3, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));
			Assert.IsTrue(distinctInputs[2].Match('c'));

		
		}



		[TestMethod]
		public void ShouldNotGetDistinctNonTerminal()
		{
			SituationProducer<char> producer;

			producer = new SituationProducer<char>();
			Assert.ThrowsException<ArgumentNullException>(() => producer.GetNextNonTerminals(null));
		}

		[TestMethod]
		public void ShouldGetOneDistinctNonTerminal()
		{
			Situation2<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A={A}|{A}|{A}");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation2<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			
		}
		[TestMethod]
		public void ShouldGetTwoDistinctNonTerminal()
		{
			Situation2<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A={A}|{A}|{B}");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation2<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(2, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			Assert.AreEqual("B", distinctInputs[1]);
		}
		[TestMethod]
		public void ShouldGetThreeDistinctNonTerminal()
		{
			Situation2<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			Rule<char> rule;

			rule = RuleHelper.BuildRule("A={A}|{B}|{C}");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation2<char>[] { s1, s2, s3 }).ToArray();
			Assert.AreEqual(3, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			Assert.AreEqual("B", distinctInputs[1]);
			Assert.AreEqual("C", distinctInputs[2]);
		}


		[TestMethod]
		public void ShouldNotGetNextSituations()
		{
			SituationProducer<char> producer;
			Situation2<char> s1, s2, s3;
			MockedSituationGraph situationGraph;
			Rule<char> rule;

			situationGraph = new MockedSituationGraph();
			rule = RuleHelper.BuildRule("A=a|a|a");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			Assert.ThrowsException<ArgumentNullException>(() => producer.GetNextSituations(situationGraph, new Situation2<char>[] { s1, s2, s3 },null));
			Assert.ThrowsException<ArgumentNullException>(() => producer.GetNextSituations(situationGraph, null, new TerminalInput<char>() { Value = 'a' }).ToArray());
			Assert.ThrowsException<ArgumentNullException>(() => producer.GetNextSituations(null, new Situation2<char>[] { s1, s2, s3 }, new TerminalInput<char>() { Value = 'a' }).ToArray());

		}


		[TestMethod]
		public void ShouldGetNextSituations()
		{
			SituationProducer<char> producer;
			Situation2<char> s1, s2, s3;
			Situation2<char>[] nextSituations;
			MockedSituationGraph situationGraph;
			Rule<char> rule;

			situationGraph = new MockedSituationGraph();
			rule = RuleHelper.BuildRule("A=a|a|a");

			s1 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[0] as InputPredicate<char> };
			s2 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[1] as InputPredicate<char> };
			s3 = new Situation2<char>() { Rule = rule, Predicate = (rule.Predicate as Or<char>).Items[2] as InputPredicate<char> };

			producer = new SituationProducer<char>();
			nextSituations = producer.GetNextSituations(situationGraph, new Situation2<char>[] { s1, s2, s3 }, new TerminalInput<char>() { Value = 'a' }).ToArray();

			Assert.AreEqual(1, nextSituations.Length);
			Assert.AreEqual(situationGraph.MockedPredicate,nextSituations[0].Predicate);
		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			SituationProducer<char> producer;
			State<char> a, b;
			ShiftOnTerminal<char> action;

			a = new State<char>();
			b = new State<char>(); 

			action = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = 1 };

			producer = new SituationProducer<char>();
			producer.Connect(a.AsEnumerable(), action.AsEnumerable());

			Assert.AreEqual(1, a.TerminalActions.Count);
			Assert.AreEqual(0, b.TerminalActions.Count);
			Assert.AreEqual(1, a.TerminalActions[0].TargetStateIndex);
			Assert.IsTrue(a.TerminalActions[0].Input.Match('a'));
		}



		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			SituationProducer<char> producer;
			State<char> a, b,c;
			ShiftOnTerminal<char> action1,action2;

			a = new State<char>();
			b = new State<char>();
			c = new State<char>();

			action1 = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = 1 };
			action2 = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'b' }, TargetStateIndex = 2 };

			producer = new SituationProducer<char>();
			producer.Connect(a.AsEnumerable(), new BaseAction<char>[] { action1,action2} );


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
			SituationProducer<char> producer;
			State<char> a, b, c;
			ShiftOnTerminal<char> action1;

			a = new State<char>();
			b = new State<char>();
			c = new State<char>();

			action1 = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = 2 };

			producer = new SituationProducer<char>();
			producer.Connect(new State<char>[] { a,b}, action1.AsEnumerable());
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
			SituationProducer<char> producer;
			State<char> a, b, c, d;
			ShiftOnTerminal<char> action1, action2;

			a = new State<char>();
			b = new State<char>();
			c = new State<char>();
			d = new State<char>();

			action1 = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = 2 };
			action2 = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'b' }, TargetStateIndex = 3 };

			producer = new SituationProducer<char>();
			producer.Connect(new State<char>[] { a, b }, new BaseAction<char>[] { action1, action2 });

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
			SituationProducer<char> producer;
			State<char> a, b;
			ShiftOnTerminal<char> shiftTerminal;
			ShiftOnNonTerminal<char> shiftNonTerminal;
			Reduce<char> reduce;

			a = new State<char>();
			b = new State<char>();

			shiftTerminal = new ShiftOnTerminal<char>() { Input = new TerminalInput<char>() { Value = 'a' }, TargetStateIndex = 1 };
			shiftNonTerminal = new ShiftOnNonTerminal<char>() { Name="A"};
			reduce = new Reduce<char>();

			producer = new SituationProducer<char>();

			producer.Connect(a.AsEnumerable(), shiftTerminal.AsEnumerable());
			Assert.AreEqual(1, a.TerminalActions.Count);
			producer.Connect(a.AsEnumerable(), shiftTerminal.AsEnumerable());
			Assert.AreEqual(1, a.TerminalActions.Count);

			producer.Connect(a.AsEnumerable(), shiftNonTerminal.AsEnumerable());
			Assert.AreEqual(1, a.NonTerminalActions.Count);
			producer.Connect(a.AsEnumerable(), shiftNonTerminal.AsEnumerable());
			Assert.AreEqual(1, a.NonTerminalActions.Count);

			producer.Connect(a.AsEnumerable(), reduce.AsEnumerable());
			Assert.AreEqual(1, a.ReductionActions.Count);
			producer.Connect(a.AsEnumerable(), reduce.AsEnumerable());
			Assert.AreEqual(1, a.ReductionActions.Count);


		}

		[TestMethod]
		public void ShouldDevelop()
		{
			SituationProducer<char> producer;
			Rule<char> A, B;
			Situation2<char> a;
			Situation2<char>[] items;
			SituationGraph<char> graph;

			A = RuleHelper.BuildRule("A={B}");
			B = RuleHelper.BuildRule("B=a");

			a = new Situation2<char>() { Rule=A,Predicate=A.Predicate as InputPredicate<char> };
			graph = new SituationGraph<char>(new BasePredicate<char>[] { A.Predicate, B.Predicate });

			producer = new SituationProducer<char>();
			items = producer.Develop(graph,a.AsEnumerable(), new Rule<char>[] { A, B }).ToArray();

			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(A, items[0].Rule);
			Assert.AreEqual(B, items[1].Rule);
			Assert.AreEqual(B.Predicate, items[1].Predicate);
		}
		[TestMethod]
		public void ShouldDevelopRecursively()
		{
			SituationProducer<char> producer;
			Rule<char> A, B,C;
			Situation2<char> a;
			Situation2<char>[] items;
			SituationGraph<char> graph;

			A = RuleHelper.BuildRule("A={B}");
			B = RuleHelper.BuildRule("B={C}");
			C = RuleHelper.BuildRule("C=a");

			a = new Situation2<char>() { Rule = A, Predicate = A.Predicate as InputPredicate<char> };
			graph = new SituationGraph<char>(new BasePredicate<char>[] { A.Predicate, B.Predicate,C.Predicate });

			producer = new SituationProducer<char>();
			items = producer.Develop(graph, a.AsEnumerable(), new Rule<char>[] { A, B ,C}).ToArray();

			Assert.AreEqual(3, items.Length);
			Assert.AreEqual(A, items[0].Rule);
			Assert.AreEqual(B, items[1].Rule);
			Assert.AreEqual(B.Predicate, items[1].Predicate);
			Assert.AreEqual(C, items[2].Rule);
			Assert.AreEqual(C.Predicate, items[2].Predicate);
		}

	}
}
