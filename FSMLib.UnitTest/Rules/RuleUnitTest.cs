using System;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Rules
{
	[TestClass]
	public class RuleUnitTest
	{
		[TestMethod]
		public void ShouldBeHumanReadable()
		{
			Rule<char> rule;
			Sequence<char> predicate;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new Rule<char>() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abcd)", rule.ToString());
		}

		[TestMethod]
		public void ShouldBeHumanReadableWithBullet()
		{
			Rule<char> rule;
			Sequence<char> predicate;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new Rule<char>() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abc•d)", rule.ToString(item));

			//Assert.AreEqual("A=(abcd)¤", rule.ToString(ReducePredicate<char>.Instance));
		}
		[TestMethod]
		public void ShouldBeEquals()
		{
			Rule<char> rule1;
			Rule<char> rule2;

			rule1 = RuleHelper.BuildRule("A*=a");
			rule2 = RuleHelper.BuildRule("A*=a");
			Assert.IsTrue(rule1.Equals(rule2));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			Rule<char> rule1;
			Rule<char> rule2;
			Rule<char> rule3;
			Rule<char> rule4;

			rule1 = RuleHelper.BuildRule("A*=a");
			rule2 = RuleHelper.BuildRule("A*=b");
			rule3 = RuleHelper.BuildRule("A=a");
			rule4 = RuleHelper.BuildRule("B*=a");

			Assert.IsFalse(rule1.Equals(null));
			Assert.IsFalse(rule1.Equals(rule2));
			Assert.IsFalse(rule1.Equals(rule3));
			Assert.IsFalse(rule1.Equals(rule4));
		}

	}

}