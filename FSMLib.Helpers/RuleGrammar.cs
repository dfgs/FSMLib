using FSMLib.Predicates;
using FSMLib.Rules;
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
		private static readonly Parser<char> OpenBracket = Parse.Char('{');
		private static readonly Parser<char> CloseBracket = Parse.Char('}');
		private static readonly Parser<char> BackSlash = Parse.Char('\\');
		private static readonly Parser<char> Dot = Parse.Char('.');
		private static readonly Parser<char> Plus = Parse.Char('+');
		private static readonly Parser<char> Star = Parse.Char('*');
		private static readonly Parser<char> QuestionMark = Parse.Char('?');
		private static readonly Parser<char> Pipe = Parse.Char('|');

		private static Parser<char> SpecialChar = OpenBracket.Or(CloseBracket).Or(BackSlash).Or(Dot).Or(Plus).Or(Star).Or(QuestionMark).Or(Pipe);
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

		private static readonly Parser<BasePredicate<char>> SmallestPredicate =
			Terminal.Or<BasePredicate<char>>(Terminal)
			.Or<BasePredicate<char>>(AnyTerminal)
			.Or<BasePredicate<char>>(NonTerminal);
			   

		public static readonly Parser<OneOrMore<char>> OneOrMore =
			from value in SmallestPredicate
			from _ in Plus
			select new OneOrMore<char>() { Item = value };

		public static readonly Parser<ZeroOrMore<char>> ZeroOrMore =
			from value in SmallestPredicate
			from _ in Star
			select new ZeroOrMore<char>() { Item = value };

		public static readonly Parser<Optional<char>> Optional =
			from value in SmallestPredicate
			from _ in QuestionMark
			select new Optional<char>() { Item = value };

		public static readonly Parser<BasePredicate<char>> SinglePredicate =
			Optional.Or<BasePredicate<char>>(ZeroOrMore).Or(OneOrMore).Or(Terminal).Or(AnyTerminal).Or(NonTerminal);

		public static readonly Parser<Sequence<char>> Sequence =
			from firstItem in SinglePredicate
			from items in SinglePredicate.AtLeastOnce()
			select (Sequence<char>)(firstItem.AsEnumerable().Concat(items)).ToArray();


		private static readonly Parser<BasePredicate<char>> OrItem =
			from _ in Pipe
			from value in  Sequence.Or(SinglePredicate)
			select value;
		public static readonly Parser<Or<char>> Or =
			from firstPredicate in Sequence.Or(SinglePredicate)
			from otherPredicates in OrItem.AtLeastOnce()
			select (Or<char>)(new BasePredicate<char>[] { firstPredicate }.Concat(otherPredicates)).ToArray();




		public static readonly Parser<Rule<char>> NonAxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<BasePredicate<char>>(Sequence).Or(SinglePredicate)
			select new Rule<char>() { Name = name, Predicate = predicate, IsAxiom =false };

		public static readonly Parser<Rule<char>> AxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('*')
			from __ in Parse.WhiteSpace.Many()
			from ___ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<BasePredicate<char>>(Sequence).Or(SinglePredicate)
			select new Rule<char>() { Name = name, Predicate = predicate,IsAxiom=true };

		public static readonly Parser<Rule<char>> Rule =
			AxiomRule.Or(NonAxiomRule);

	}
}
