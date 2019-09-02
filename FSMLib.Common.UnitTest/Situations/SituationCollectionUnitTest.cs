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
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Situations;

namespace FSMLib.Common.UnitTest.Situations
{
	
	[TestClass]
	public class SituationCollectionUnitTest
	{
		

		
		[TestMethod]

		public void ShouldAdd()
		{
			IRule<char> rule;
			Situation<char> a,b;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };

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
			IRule<char> rule;
			Situation<char> a, b,c;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };
			c = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };

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
			IRule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };
			c = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[2] as Terminal };

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
			IRule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations1,situations2;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };
			c = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[2] as Terminal };

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
			IRule<char> rule;
			Situation<char> a, b, c;
			SituationCollection<char> situations1, situations2;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal };
			c = new Situation<char>() { Rule = rule, Predicate = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[2] as Terminal };

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
			IRule<char> rule1,rule2;
			Situation<char> a, b;
			SituationCollection<char> situations;
			ISituation<char>[] reductions;
			ITerminalInput<char> input;

			input = new TerminalInput('a');

			rule1 = RuleHelper.BuildRule("A=abc;");
			rule2 = RuleHelper.BuildRule("B=abc;");
			a = new Situation<char>() { Rule = rule1, Predicate = new Reduce(), Input=input };
			b = new Situation<char>() { Rule = rule1, Predicate = new Reduce(), Input = input };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions =situations.GetReductionSituations().ToArray();
			Assert.AreEqual(2, reductions.Length);


			a = new Situation<char>() { Rule = rule1, Predicate = new Reduce(), Input = input };
			b = new Situation<char>() { Rule = rule2, Predicate = new Reduce(), Input = input };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(2, reductions.Length);
		}

		/*[TestMethod]
		public void ShouldNotGetReductionSituationsWhenInputIsNull()
		{
			IRule<char> rule1, rule2;
			Situation<char> a, b;
			SituationCollection<char> situations;
			Situation<char>[] reductions;

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");
			a = new Situation<char>() { Rule = rule1, Predicate = new Reduce() };
			b = new Situation<char>() { Rule = rule1, Predicate = new Reduce() };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(0, reductions.Length);


			a = new Situation<char>() { Rule = rule1, Predicate = new Reduce() };
			b = new Situation<char>() { Rule = rule2, Predicate = new Reduce() };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(0, reductions.Length);
		}*/

	


	}
}
