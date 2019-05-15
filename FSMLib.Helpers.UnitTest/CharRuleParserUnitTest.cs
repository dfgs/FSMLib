using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using Sprache;
using FSMLib.Predicates;

namespace FSMLib.Helpers.UnitTest
{
	[TestClass]
	public class CharRuleParserUnitTest
	{
		


		[TestMethod]
		public void ShouldCreateRuleFromSingleChar()
		{
			CharRuleParser parser;
			Rule<char> rule;

			parser = new CharRuleParser();
			rule = parser.Parse("A=a");
			Assert.AreEqual("A", rule.Name);
			Assert.IsInstanceOfType(rule.Predicate, typeof(Terminal<char>));
		}
		[TestMethod]
		public void ShouldCreateRuleFromCharSequence()
		{
			CharRuleParser parser;
			Rule<char> rule;

			parser = new CharRuleParser();
			rule = parser.Parse("A=abcd");
			Assert.AreEqual("A", rule.Name);
			Assert.IsInstanceOfType(rule.Predicate, typeof(Sequence<char>));
		}
		[TestMethod]
		public void ShouldCreateRuleFromComplexSequence()
		{
			CharRuleParser parser;
			Rule<char> rule;

			parser = new CharRuleParser();
			rule = parser.Parse("A=ab?c+{B}d*");
			Assert.AreEqual("A", rule.Name);
			Assert.IsInstanceOfType(rule.Predicate, typeof(Sequence<char>));
		}
		[TestMethod]
		public void ShouldCreateRuleFromNonTerminal()
		{
			CharRuleParser parser;
			Rule<char> rule;

			parser = new CharRuleParser();
			rule = parser.Parse("A={B}");
			Assert.AreEqual("A", rule.Name);
			Assert.IsInstanceOfType(rule.Predicate, typeof(NonTerminal<char>));
		}
	}
}
