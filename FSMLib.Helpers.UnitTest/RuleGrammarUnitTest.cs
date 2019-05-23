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
			NonTerminal<char> result;
			result = RuleGrammar.NonTerminal.Parse("{test}");
			Assert.AreEqual("test", result.Name);
		}

		[TestMethod]
		public void ShouldParseTerminal()
		{
			Terminal<char> result;
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
			Terminal<char> result;
			result = RuleGrammar.Terminal.Parse(@"\a");
			Assert.AreEqual('a', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\{");
			Assert.AreEqual('{', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\}");
			Assert.AreEqual('}', result.Value);
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
			AnyTerminal<char> result;
			result = RuleGrammar.AnyTerminal.Parse(".");
			Assert.IsNotNull(result);
		}
		[TestMethod]
		public void ShouldParseTerminalSequence()
		{
			Sequence<char> result;

			result = RuleGrammar.TerminalSequence.Parse("abcd");
			Assert.AreEqual(4,result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.TerminalSequence.Parse(@"ab\.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.TerminalSequence.Parse(@"ab.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.TerminalSequence.Parse(@" b. "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.TerminalSequence.Parse(@"ab?d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Optional<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
		}


		[TestMethod]
		public void ShouldParseOneOrMore()
		{
			OneOrMore<char> result;
			result = RuleGrammar.OneOrMore.Parse("a+");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal<char>));
			result = RuleGrammar.OneOrMore.Parse(".+");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal<char>));
		}
		[TestMethod]
		public void ShouldParseZeroOrMore()
		{
			ZeroOrMore<char> result;
			result = RuleGrammar.ZeroOrMore.Parse("a*");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal<char>));
			result = RuleGrammar.ZeroOrMore.Parse(".*");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal<char>));
		}
		[TestMethod]
		public void ShouldParseOptional()
		{
			Optional<char> result;
			result = RuleGrammar.Optional.Parse("a?");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal<char>));
			result = RuleGrammar.Optional.Parse(".?");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal<char>));
		}


		[TestMethod]
		public void ShouldParseRule()
		{
			Rule<char> result;

			result = RuleGrammar.Rule.Parse("A=a");
			Assert.IsInstanceOfType(result.Predicate, typeof(Terminal<char>));
			Assert.AreEqual("A", result.Name);
			result = RuleGrammar.Rule.Parse("A=a?");
			Assert.IsInstanceOfType(result.Predicate, typeof(Optional<char>));
			Assert.AreEqual("A", result.Name);
			result = RuleGrammar.Rule.Parse("A=abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence<char>));
			Assert.AreEqual("A", result.Name);
			result = RuleGrammar.Rule.Parse("A = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence<char>));
			Assert.AreEqual("A", result.Name);
			result = RuleGrammar.Rule.Parse("ABC = abcd");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence<char>));
			Assert.AreEqual("ABC", result.Name);


		}

		[TestMethod]
		public void ShouldParseOr()
		{
			Or<char> result;

			result = RuleGrammar.Or.Parse("a|b|c|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.Or.Parse(@"a|b|\.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.Or.Parse(@"a|b|.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.Or.Parse(@" |b|.| "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal<char>));

			result = RuleGrammar.Or.Parse("a|bcd|d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(Sequence<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));

			result = RuleGrammar.Or.Parse("a|{b}|c|{d}");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[1], typeof(NonTerminal<char>));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal<char>));
			Assert.IsInstanceOfType(result.Items[3], typeof(NonTerminal<char>));


		}



	}
}
