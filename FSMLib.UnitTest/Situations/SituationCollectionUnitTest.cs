using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.Inputs;

namespace FSMLib.UnitTest.Situations
{
	
	[TestClass]
	public class SituationCollectionUnitTest
	{
		

		
		[TestMethod]

		public void ShouldAdd()
		{
			Rule<char> rule;
			Situation<char> a,b;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };

			situations = new SituationCollection<char>();
			Assert.AreEqual(0, situations.Count);
			situations.Add(a);
			Assert.AreEqual(1, situations.Count);
			situations.Add(b);
			Assert.AreEqual(2, situations.Count);
		}

		[TestMethod]

		public void ShouldNotAddDuplicates()
		{
			Rule<char> rule;
			Situation<char> a, b,c;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };

			situations = new SituationCollection<char>();
			Assert.AreEqual(0, situations.Count);
			situations.Add(a);
			Assert.AreEqual(1, situations.Count);
			situations.Add(b);
			Assert.AreEqual(2, situations.Count);
			situations.Add(c);
			Assert.AreEqual(2, situations.Count);
		}

		[TestMethod]

		public void ShouldContains()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[2] as Terminal<char> };

			situations = new SituationCollection<char>();
			situations.Add(a);
			situations.Add(b);

			Assert.IsTrue(situations.Contains(a));
			Assert.IsTrue(situations.Contains(b));
			Assert.IsFalse(situations.Contains(c));
		}
		[TestMethod]

		public void ShouldEquals()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations1,situations2;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[2] as Terminal<char> };

			situations1 = new SituationCollection<char>();
			situations1.Add(a);
			situations1.Add(b);
			situations1.Add(c);

			situations2 = new SituationCollection<char>();	// different order
			situations2.Add(c);
			situations2.Add(a);
			situations2.Add(b);

			Assert.IsTrue(situations1.Equals(situations2));
			Assert.IsTrue(situations2.Equals(situations1));
		}

		[TestMethod]

		public void ShouldNotEquals()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations1, situations2;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[2] as Terminal<char> };

			situations1 = new SituationCollection<char>();
			situations1.Add(a);
			situations1.Add(b);
			situations1.Add(c);

			situations2 = new SituationCollection<char>();  // different order
			situations2.Add(c);
			situations2.Add(b);

			Assert.IsFalse(situations1.Equals(situations2));
			Assert.IsFalse(situations2.Equals(situations1));
		}

		[TestMethod]

		public void ShouldGetReductionSituations()
		{
			Rule<char> rule1,rule2;
			Situation<char> a, b;
			SituationCollection<char> situations;
			Situation<char>[] reductions;
			TerminalInput<char> input;

			input = new TerminalInput<char>();

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");
			a = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>(), Input=input };
			b = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>(), Input = input };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions =situations.GetReductionSituations().ToArray();
			Assert.AreEqual(2, reductions.Length);


			a = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>(), Input = input };
			b = new Situation<char>() { Rule = rule2, Predicate = new ReducePredicate<char>(), Input = input };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(2, reductions.Length);
		}

		[TestMethod]
		public void ShouldNotGetReductionSituationsWhenInputIsNull()
		{
			Rule<char> rule1, rule2;
			Situation<char> a, b;
			SituationCollection<char> situations;
			Situation<char>[] reductions;

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");
			a = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>() };
			b = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>() };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(0, reductions.Length);


			a = new Situation<char>() { Rule = rule1, Predicate = new ReducePredicate<char>() };
			b = new Situation<char>() { Rule = rule2, Predicate = new ReducePredicate<char>() };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(0, reductions.Length);
		}

		[TestMethod]

		public void ShouldGetDistinctOneNextInputs()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations;
			IInput<char>[] inputs;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };

			situations = new SituationCollection<char>();
			situations.Add(a);
			situations.Add(b);
			situations.Add(c);

			inputs = situations.GetNextInputs().ToArray();
			Assert.AreEqual(1, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
		}

		[TestMethod]

		public void ShouldGetDistinctTwoNextInputs()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations;
			IInput<char>[] inputs;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };

			situations = new SituationCollection<char>();
			situations.Add(a);
			situations.Add(b);
			situations.Add(c);

			inputs = situations.GetNextInputs().ToArray();
			Assert.AreEqual(2, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
			Assert.IsTrue(inputs[1].Match('b'));
		}


		public void ShouldGetDistinctThreeNextInputs()
		{
			Rule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations;
			IInput<char>[] inputs;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char> };
			c = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence<char>).Items[2] as Terminal<char> };

			situations = new SituationCollection<char>();
			situations.Add(a);
			situations.Add(b);
			situations.Add(c);

			inputs = situations.GetNextInputs().ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
			Assert.IsTrue(inputs[1].Match('b'));
			Assert.IsTrue(inputs[2].Match('c'));
		}


	}
}
