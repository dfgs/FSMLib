using System;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.SyntaxicAnalysis.Rules;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.SyntaxisAnalysis;
using FSMLib.SyntaxicAnalysis.Helpers;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Rules
{
	[TestClass]
	public class RuleUnitTest
	{
		[TestMethod]
		public void ShouldBeHumanReadable()
		{
			SyntaxicRule rule;
			Sequence predicate;
			Terminal item;

			predicate = new Sequence();
			item = new Terminal(new Token("C","a") );
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "b"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "c"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "d"));
			predicate.Items.Add(item);

			rule = new SyntaxicRule() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(<C,a><C,b><C,c><C,d>)", rule.ToString());
		}

		[TestMethod]
		public void ShouldBeHumanReadableWithBullet()
		{
			SyntaxicRule rule;
			Sequence predicate;
			Terminal item;

			predicate = new Sequence();
			item = new Terminal(new Token("C", "a"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "b"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "c"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "d"));
			predicate.Items.Add(item);

			rule = new SyntaxicRule() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(<C,a><C,b><C,c>•<C,d>)", rule.ToString(item));

		}
		[TestMethod]
		public void ShouldBeHumanReadableWhenIsAxiom()
		{
			SyntaxicRule rule;
			Sequence predicate;
			Terminal item;

			predicate = new Sequence();
			item = new Terminal(new Token("C", "a"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "b"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "c"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "d"));
			predicate.Items.Add(item);

			rule = new SyntaxicRule() { Name = "A",IsAxiom=true };
			rule.Predicate = predicate;

			Assert.AreEqual("A*=(<C,a><C,b><C,c><C,d>)", rule.ToString());
		}
		[TestMethod]
		public void ShouldBeHumanReadableWhenIsAxiomWithBullet()
		{
			SyntaxicRule rule;
			Sequence predicate;
			Terminal item;

			predicate = new Sequence();
			item = new Terminal(new Token("C", "a"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "b"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "c"));
			predicate.Items.Add(item);
			item = new Terminal(new Token("C", "d"));
			predicate.Items.Add(item);

			rule = new SyntaxicRule() { Name = "A",IsAxiom=true };
			rule.Predicate = predicate;

			Assert.AreEqual("A*=(<C,a><C,b><C,c>•<C,d>)", rule.ToString(item));

		}
		[TestMethod]
		public void ShouldBeEquals()
		{
			SyntaxicRule rule1;
			SyntaxicRule rule2;

			rule1 = RuleHelper.BuildRule("A*=a;");
			rule2 = RuleHelper.BuildRule("A*=a;");
			Assert.IsTrue(rule1.Equals(rule2));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			SyntaxicRule rule1;
			SyntaxicRule rule2;
			SyntaxicRule rule3;
			SyntaxicRule rule4;

			rule1 = RuleHelper.BuildRule("A*=a;");
			rule2 = RuleHelper.BuildRule("A*=b;");
			rule3 = RuleHelper.BuildRule("A=a;");
			rule4 = RuleHelper.BuildRule("B*=a;");

			Assert.IsFalse(rule1.Equals(null));
			Assert.IsFalse(rule1.Equals(rule2));
			Assert.IsFalse(rule1.Equals(rule3));
			Assert.IsFalse(rule1.Equals(rule4));
		}

	}

}