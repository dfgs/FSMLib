using System;
using FSMLib.Helpers;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.LexicalAnalysis.UnitTest.Rules
{
	[TestClass]
	public class RuleUnitTest
	{
		[TestMethod]
		public void ShouldBeHumanReadable()
		{
			LexicalRule rule;
			Sequence predicate;
			Letter item;

			predicate = new Sequence();
			item = new Letter() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new LexicalRule() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abcd)", rule.ToString());
		}

		[TestMethod]
		public void ShouldBeHumanReadableWithBullet()
		{
			LexicalRule rule;
			Sequence predicate;
			Letter item;

			predicate = new Sequence();
			item = new Letter() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new LexicalRule() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abc•d)", rule.ToString(item));

			//Assert.AreEqual("A=(abcd)¤", rule.ToString(Reduce.Instance));
		}
		[TestMethod]
		public void ShouldBeHumanReadableWhenIsAxiom()
		{
			LexicalRule rule;
			Sequence predicate;
			Letter item;

			predicate = new Sequence();
			item = new Letter() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new LexicalRule() { Name = "A",IsAxiom=true };
			rule.Predicate = predicate;

			Assert.AreEqual("A*=(abcd)", rule.ToString());
		}
		[TestMethod]
		public void ShouldBeHumanReadableWhenIsAxiomWithBullet()
		{
			LexicalRule rule;
			Sequence predicate;
			Letter item;

			predicate = new Sequence();
			item = new Letter() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Letter() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new LexicalRule() { Name = "A",IsAxiom=true };
			rule.Predicate = predicate;

			Assert.AreEqual("A*=(abc•d)", rule.ToString(item));

			//Assert.AreEqual("A=(abcd)¤", rule.ToString(Reduce.Instance));
		}
		[TestMethod]
		public void ShouldBeEquals()
		{
			LexicalRule rule1;
			LexicalRule rule2;

			rule1 = RuleHelper.BuildRule("A*=a");
			rule2 = RuleHelper.BuildRule("A*=a");
			Assert.IsTrue(rule1.Equals(rule2));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			LexicalRule rule1;
			LexicalRule rule2;
			LexicalRule rule3;
			LexicalRule rule4;

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