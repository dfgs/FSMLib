using FSMLib.Predicates;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
    public class RuleGrammar
    {
		private static readonly Parser<char> OpenBracket = Parse.Char('<');
		private static readonly Parser<char> CloseBracket = Parse.Char('>');
		private static readonly Parser<char> BackSlash = Parse.Char('\\');
		private static readonly Parser<char> Dot = Parse.Char('.');
		private static readonly Parser<char> Plus = Parse.Char('+');
		private static readonly Parser<char> Star = Parse.Char('*');
		private static readonly Parser<char> QuestionMark = Parse.Char('?');

		private static Parser<char> SpecialChar = OpenBracket.Or(CloseBracket).Or(BackSlash).Or(Dot).Or(Plus).Or(Star).Or(QuestionMark);
		private static Parser<char> NormalChar = Parse.AnyChar.Except(SpecialChar);

		private static readonly Parser<char> EscapedChar =
			from _ in BackSlash
			from c in Parse.AnyChar
			select c;


		public static readonly Parser<Terminal<char>> Terminal =
			from value in NormalChar.Or(EscapedChar)
			select new Terminal<char>() {Value = value };

		public static readonly Parser<NonTerminal<char>> NonTerminal =
			from open in OpenBracket
			from name in Parse.AnyChar.Except(CloseBracket).Many().Text().Token()
			from close in CloseBracket
			select new NonTerminal<char>() { Name = name };

		public static readonly Parser<AnyTerminal<char>> AnyTerminal =
			from _ in Dot
			select new AnyTerminal<char>();

		private static readonly Parser<BasePredicate<char>> TerminalPredicate =
			Terminal.Or<BasePredicate<char>>(NonTerminal)
			.Or<BasePredicate<char>>(AnyTerminal);

		public static readonly Parser<Sequence<char>> TerminalSequence =
			from firstPredicate in TerminalPredicate
			from otherPredicates in TerminalPredicate.AtLeastOnce()
			select (Sequence<char>)(new BasePredicate<char>[] { firstPredicate }.Concat(otherPredicates) ).ToArray();


		public static readonly Parser<OneOrMore<char>> OneOrMore =
			from value in TerminalPredicate
			from _ in Plus
			select new OneOrMore<char>() { Item = value };

		public static readonly Parser<ZeroOrMore<char>> ZeroOrMore =
			from value in TerminalPredicate
			from _ in Star
			select new ZeroOrMore<char>() { Item = value };

		public static readonly Parser<Optional<char>> Optional =
			from value in TerminalPredicate
			from _ in QuestionMark
			select new Optional<char>() { Item = value };

		private static readonly Parser<BasePredicate<char>> AdvancedPredicate =
			TerminalSequence.Or<BasePredicate<char>>(OneOrMore)
			.Or<BasePredicate<char>>(ZeroOrMore).Or(Optional);

		private static readonly Parser<BasePredicate<char>> RuleSinglePredicate =
			from predicate in AdvancedPredicate.Or(TerminalPredicate)
			select predicate;

		private static readonly Parser<Sequence<char>> RuleSequencePredicate =
			from predicates in (AdvancedPredicate.Or(TerminalPredicate)).Many()
			select (Sequence<char>)predicates.ToArray();

		public static readonly Parser<BasePredicate<char>> RulePredicate =
			from predicate in RuleSinglePredicate.Or(RuleSequencePredicate)
			select predicate;

	}
}
