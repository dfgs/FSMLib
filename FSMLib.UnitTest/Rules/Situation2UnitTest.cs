using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class Situation2UnitTest
	{
		[TestMethod]
		public void ShouldBeEquals()
		{
			Situation2<char> a, b;
			Rule<char> rule;
			InputPredicate<char> predicate;

			rule = RuleHelper.BuildRule("A=abc");
			predicate=(rule.Predicate as Sequence<char>).Items[0] as Terminal<char>;
			a = new Situation2<char>() { Rule = rule, Predicate = predicate };
			b = new Situation2<char>() { Rule = rule, Predicate = predicate };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			Situation2<char> a, b;
			Rule<char> rule;
			InputPredicate<char> predicate1,predicate2;

			rule = RuleHelper.BuildRule("A=abc");
			predicate1 = (rule.Predicate as Sequence<char>).Items[0] as Terminal<char>;
			predicate2 = (rule.Predicate as Sequence<char>).Items[1] as Terminal<char>;
			a = new Situation2<char>() { Rule = rule, Predicate = predicate1 };
			b = new Situation2<char>() { Rule = rule, Predicate = predicate2 };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}



	}
}
