using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using Sprache;
using FSMLib.Predicates;
using FSMLib.Rules;

namespace FSMLib.Helpers.UnitTest
{
	[TestClass]
	public class RuleGrammarUnitTest
	{
		


		[TestMethod]
		public void ShouldParseNonTerminal()
		{
			NonTerminalPredicate<char> result;
			result = RuleGrammar.NonTerminal.Parse("{test}");
			Assert.AreEqual("test", result.Name);
		}

		[TestMethod]
		public void ShouldParseTerminal()
		{
			TerminalPredicate<char> result;
			result = RuleGrammar.Terminal.Parse("a");
			Assert.AreEqual('a', result.Value);
			result = RuleGrammar.Terminal.Parse("1");
			Assert.AreEqual('1', result.Value);
			result = RuleGrammar.Terminal.Parse("-");
			Assert.AreEqual('-', result.Value);
		}
		[TestMethod]
		public void ShouldParseTerminalFromEscapedChar()
		{
			TerminalPredicate<char> result;
			result = RuleGrammar.Terminal.Parse(@"\a");
			Assert.AreEqual('a', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\{");
			Assert.AreEqual('{', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\}");
			Assert.AreEqual('}', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\[");
			Assert.AreEqual('[', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\]");
			Assert.AreEqual(']', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\.");
			Assert.AreEqual('.', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\*");
			Assert.AreEqual('*', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\?");
			Assert.AreEqual('?', result.Value);
		}
		[TestMethod]
		public void ShouldNotParseSpecialCharactersAsTerminal()
		{
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("["));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("]"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("{"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("}"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse(@"\"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("."));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("*"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("?"));
		}
		[TestMethod]
		public void ShouldParseAnyTerminal()
		{
			AnyTerminalPredicate<char> result;
			result = RuleGrammar.AnyTerminal.Parse(".");
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void ShouldParseTerminalRange()
		{
			TerminalRangePredicate<char> result;
			result = RuleGrammar.TerminalRange.Parse("[a-z]");
			Assert.AreEqual('a', result.FirstValue);
			Assert.AreEqual('z', result.LastValue);
			result = RuleGrammar.TerminalRange.Parse("[{-}]");
			Assert.AreEqual('{', result.FirstValue);
			Assert.AreEqual('}', result.LastValue);
		}

		[TestMethod]
		public void ShouldParseSequence()
		{
			SequencePredicate<char> result;

			result = RuleGrammar.Sequence.Parse("abcd");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse("ab[c-d]d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalRangePredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse(@"ab\.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse(@"ab.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse(@" b. "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse(@"ab?d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(OptionalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Sequence.Parse(@"a*b?d+");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(ZeroOrMorePredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(OptionalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(OneOrMorePredicate<char>));
		}


		[TestMethod]
		public void ShouldParseOneOrMore()
		{
			OneOrMorePredicate<char> result;
			result = RuleGrammar.OneOrMore.Parse("a+");
			Assert.IsInstanceOfType(result.Item, typeof(TerminalPredicate<char>));
			result = RuleGrammar.OneOrMore.Parse(".+");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminalPredicate<char>));
			result = RuleGrammar.OneOrMore.Parse("{A}+");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminalPredicate<char>));
		}
		[TestMethod]
		public void ShouldParseZeroOrMore()
		{
			ZeroOrMorePredicate<char> result;
			result = RuleGrammar.ZeroOrMore.Parse("a*");
			Assert.IsInstanceOfType(result.Item, typeof(TerminalPredicate<char>));
			result = RuleGrammar.ZeroOrMore.Parse(".*");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminalPredicate<char>));
			result = RuleGrammar.ZeroOrMore.Parse("{A}*");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminalPredicate<char>));
		}
		[TestMethod]
		public void ShouldParseOptional()
		{
			OptionalPredicate<char> result;
			result = RuleGrammar.Optional.Parse("a?");
			Assert.IsInstanceOfType(result.Item, typeof(TerminalPredicate<char>));
			result = RuleGrammar.Optional.Parse(".?");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminalPredicate<char>));
			result = RuleGrammar.Optional.Parse("{A}?");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminalPredicate<char>));
		}


		[TestMethod]
		public void ShouldParseNonAxiomRule()
		{
			Rule<char> result;

			result = RuleGrammar.NonAxiomRule.Parse("A=a");
			Assert.IsInstanceOfType(result.Predicate, typeof(TerminalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A=a?");
			Assert.IsInstanceOfType(result.Predicate, typeof(OptionalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A=abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("ABC = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.IsFalse(result.IsAxiom);
			Assert.AreEqual("ABC", result.Name);


		}
		[TestMethod]
		public void ShouldParseAxiomRule()
		{
			Rule<char> result;

			result = RuleGrammar.AxiomRule.Parse("A*=a");
			Assert.IsInstanceOfType(result.Predicate, typeof(TerminalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A*=a?");
			Assert.IsInstanceOfType(result.Predicate, typeof(OptionalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A*=abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A* = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("ABC* = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);


		}
		[TestMethod]
		public void ShouldParseRule()
		{
			Rule<char> result;

			result = RuleGrammar.Rule.Parse("A*=a");
			Assert.IsInstanceOfType(result.Predicate, typeof(TerminalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A=a?");
			Assert.IsInstanceOfType(result.Predicate, typeof(OptionalPredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A*=ab[c-d]d");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("ABC* = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(SequencePredicate<char>));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("ABC* = a|b|[c-d]|d");
			Assert.IsInstanceOfType(result.Predicate, typeof(OrPredicate<char>));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);


		}
		[TestMethod]
		public void ShouldParseOr()
		{
			OrPredicate<char> result;

			result = RuleGrammar.Or.Parse("a|b|c|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Or.Parse(@"a|b|\.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Or.Parse(@"a|b|.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Or.Parse(@" |b|.| "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Or.Parse("a|bcd|d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(SequencePredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalPredicate<char>));

			result = RuleGrammar.Or.Parse("a|{b}|[c-d]|{d}");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(TerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(NonTerminalPredicate<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(TerminalRangePredicate<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(NonTerminalPredicate<char>));


		}



	}
}
