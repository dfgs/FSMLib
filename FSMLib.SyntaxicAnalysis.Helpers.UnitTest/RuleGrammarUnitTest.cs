using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.SyntaxicAnalysis.Helpers;
using Sprache;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.Helpers.UnitTest
{
	[TestClass]
	public class RuleGrammarUnitTest
	{
		


		[TestMethod]
		public void ShouldParseNonTerminal()
		{
			NonTerminal result;
			result = RuleGrammar.NonTerminal.Parse("{test}");
			Assert.AreEqual("test", result.Name);
		}

		[TestMethod]
		public void ShouldParseTerminal()
		{
			Terminal result;
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
			Terminal result;
			result = RuleGrammar.Terminal.Parse(@"\a");
			Assert.AreEqual('a', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\{");
			Assert.AreEqual('{', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\}");
			Assert.AreEqual('}', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\<");
			Assert.AreEqual('<', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\>");
			Assert.AreEqual('>', result.Value);
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
			result = RuleGrammar.Terminal.Parse(@"\!");
			Assert.AreEqual('!', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\;");
			Assert.AreEqual(';', result.Value);
			result = RuleGrammar.Terminal.Parse(@"\,");
			Assert.AreEqual(',', result.Value);
		}
		[TestMethod]
		public void ShouldNotParseSpecialCharactersAsLetter()
		{
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("["));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("]"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("<"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse(">"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("{"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("}"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse(@"\"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("."));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("*"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("?"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse("!"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse(";"));
			Assert.ThrowsException<ParseException>(() => RuleGrammar.Terminal.Parse(","));
		}
		[TestMethod]
		public void ShouldParseReduce()
		{
			Reduce result;
			result = RuleGrammar.Reduce.Parse(";");
			Assert.IsNotNull(result);
		}
		[TestMethod]
		public void ShouldParseAnyTerminal()
		{
			AnyTerminal result;
			result = RuleGrammar.AnyTerminal.Parse(".");
			Assert.IsNotNull(result);
		}

	
	

		[TestMethod]
		public void ShouldParseSequence()
		{
			Sequence result;

			result = RuleGrammar.Sequence.Parse("abcd");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));


			result = RuleGrammar.Sequence.Parse(@"ab\.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Sequence.Parse(@"ab.d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Sequence.Parse(@" b. "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Sequence.Parse(@"ab?d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Optional));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));

			result = RuleGrammar.Sequence.Parse(@"a*b?d+");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(ZeroOrMore));
			Assert.IsInstanceOfType(result.Items[1], typeof(Optional));
			Assert.IsInstanceOfType(result.Items[2], typeof(OneOrMore));

		}


		[TestMethod]
		public void ShouldParseOneOrMore()
		{
			OneOrMore result;
			result = RuleGrammar.OneOrMore.Parse("a+");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal));
			result = RuleGrammar.OneOrMore.Parse(".+");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal));
			result = RuleGrammar.OneOrMore.Parse("{A}+");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminal));
		}
		[TestMethod]
		public void ShouldParseZeroOrMore()
		{
			ZeroOrMore result;
			result = RuleGrammar.ZeroOrMore.Parse("a*");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal));
			result = RuleGrammar.ZeroOrMore.Parse(".*");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal));
			result = RuleGrammar.ZeroOrMore.Parse("{A}*");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminal));
		}
		[TestMethod]
		public void ShouldParseOptional()
		{
			Optional result;
			result = RuleGrammar.Optional.Parse("a?");
			Assert.IsInstanceOfType(result.Item, typeof(Terminal));
			result = RuleGrammar.Optional.Parse(".?");
			Assert.IsInstanceOfType(result.Item, typeof(AnyTerminal));
			result = RuleGrammar.Optional.Parse("{A}?");
			Assert.IsInstanceOfType(result.Item, typeof(NonTerminal));
		}


		[TestMethod]
		public void ShouldParseNonAxiomRule()
		{
			IRule<Token> result;

			result = RuleGrammar.NonAxiomRule.Parse("A=a;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A=a?;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A=abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("A = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.NonAxiomRule.Parse("ABC = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.IsFalse(result.IsAxiom);
			Assert.AreEqual("ABC", result.Name);


		}
		[TestMethod]
		public void ShouldParseAxiomRule()
		{
			IRule<Token> result;

			result = RuleGrammar.AxiomRule.Parse("A*=a;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A*=a?;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A*=abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("A* = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.AxiomRule.Parse("ABC* = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);


		}
		[TestMethod]
		public void ShouldParseRule()
		{
			IRule<Token> result;

			result = RuleGrammar.Rule.Parse("A*=a;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A=a?;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A*=ab[c-d]d;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsFalse(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("ABC* = abcd;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("ABC* = a|b|[c-d]|d;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("ABC", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A*=a!bc;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);
			result = RuleGrammar.Rule.Parse("A*=a![b-d]c;");
			Assert.IsInstanceOfType(result.Predicate, typeof(Sequence));
			Assert.AreEqual("A", result.Name);
			Assert.IsTrue(result.IsAxiom);


		}
		[TestMethod]
		public void ShouldParseOr()
		{
			Or result;

			result = RuleGrammar.Or.Parse("a|b|c|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Or.Parse(@"a|b|\.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Or.Parse(@"a|b|.|d");
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Or.Parse(@" |b|.| "); // a space at begin and end
			Assert.AreEqual(4, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[2], typeof(AnyTerminal));
			Assert.IsInstanceOfType(result.Items[3], typeof(Terminal));

			result = RuleGrammar.Or.Parse("a|bcd|d");
			Assert.AreEqual(3, result.Items.Count);
			Assert.IsInstanceOfType(result.Items[0], typeof(Terminal));
			Assert.IsInstanceOfType(result.Items[1], typeof(Sequence));
			Assert.IsInstanceOfType(result.Items[2], typeof(Terminal));

	
		}



	}
}
