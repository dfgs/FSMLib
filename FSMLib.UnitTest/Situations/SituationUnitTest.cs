using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;
using FSMLib.LexicalAnalysis.Predicates;

namespace FSMLib.UnitTest.Situations
{
	[TestClass]
	public class SituationUnitTest
	{
		[TestMethod]
		public void ShouldBeEquals()
		{
			FSMLib.Situations.Situation<char> a, b;
			IRule<char> rule;
			ISituationPredicate<char> predicate;

			rule = RuleHelper.BuildRule("A=abc;");
			predicate=(rule.Predicate as Sequence).Items[0] as Terminal;
			a = new FSMLib.Situations.Situation<char>() { Rule = rule, Predicate = predicate };
			b = new FSMLib.Situations.Situation<char>() { Rule = rule, Predicate = predicate };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			FSMLib.Situations.Situation<char> a, b;
			IRule<char> rule;
			ISituationPredicate<char> predicate1,predicate2;

			rule = RuleHelper.BuildRule("A=abc;");
			predicate1 = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[0] as Terminal;
			predicate2 = ((rule.Predicate as Sequence).Items[0] as Sequence).Items[1] as Terminal;
			a = new FSMLib.Situations.Situation<char>() { Rule = rule, Predicate = predicate1 };
			b = new FSMLib.Situations.Situation<char>() { Rule = rule, Predicate = predicate2 };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}



	}
}
