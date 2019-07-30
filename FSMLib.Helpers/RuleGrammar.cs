using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;
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
		private static readonly Parser<char> OpenBrace = Parse.Char('{');
		private static readonly Parser<char> CloseBrace = Parse.Char('}');
		private static readonly Parser<char> OpenBracket = Parse.Char('[');
		private static readonly Parser<char> CloseBracket = Parse.Char(']');
		private static readonly Parser<char> BackSlash = Parse.Char('\\');
		private static readonly Parser<char> Dot = Parse.Char('.');
		private static readonly Parser<char> Plus = Parse.Char('+');
		private static readonly Parser<char> Star = Parse.Char('*');
		private static readonly Parser<char> QuestionMark = Parse.Char('?');
		private static readonly Parser<char> Pipe = Parse.Char('|');

		private static Parser<char> SpecialChar = OpenBracket.Or(CloseBracket).Or(OpenBrace).Or(CloseBrace).Or(BackSlash).Or(Dot).Or(Plus).Or(Star).Or(QuestionMark).Or(Pipe);
		private static Parser<char> NormalChar = Parse.AnyChar.Except(SpecialChar);

		private static readonly Parser<char> EscapedChar =
			from _ in BackSlash
			from c in Parse.AnyChar
			select c;


		public static readonly Parser<Letter> Terminal =
			from value in NormalChar.Or(EscapedChar)
			select new Letter(value );

		public static readonly Parser<NonTerminal> NonTerminal =
			from open in OpenBrace
			from name in Parse.AnyChar.Except(CloseBrace).Many().Text().Token()
			from close in CloseBrace
			select new NonTerminal(name);

		public static readonly Parser<LettersRange> TerminalRange =
			from open in OpenBracket
			from FirstItem in Parse.AnyChar
			from _ in Parse.Char('-')
			from LastItem in Parse.AnyChar
			from close in CloseBracket
			select new LettersRange(FirstItem,LastItem);

		public static readonly Parser<AnyLetter> AnyTerminal =
			from _ in Dot
			select new AnyLetter();

		private static readonly Parser<LexicalPredicate> SmallestPredicate =
			Terminal.Or<LexicalPredicate>(Terminal)
			.Or<LexicalPredicate>(AnyTerminal)
			.Or<LexicalPredicate>(NonTerminal)
			.Or<LexicalPredicate>(TerminalRange);
			   

		public static readonly Parser<OneOrMore> OneOrMore =
			from value in SmallestPredicate
			from _ in Plus
			select new OneOrMore() { Item = value };

		public static readonly Parser<ZeroOrMore> ZeroOrMore =
			from value in SmallestPredicate
			from _ in Star
			select new ZeroOrMore() { Item = value };

		public static readonly Parser<Optional> Optional =
			from value in SmallestPredicate
			from _ in QuestionMark
			select new Optional() { Item = value };

		public static readonly Parser<LexicalPredicate> SinglePredicate =
			Optional.Or<LexicalPredicate>(ZeroOrMore).Or(OneOrMore).Or(Terminal).Or(AnyTerminal).Or(NonTerminal).Or(TerminalRange);

		public static readonly Parser<Sequence> Sequence =
			from firstItem in SinglePredicate
			from items in SinglePredicate.AtLeastOnce()
			select new Sequence() { Items=new List<LexicalPredicate>(firstItem.AsEnumerable().Concat(items)) } ;


		private static readonly Parser<LexicalPredicate> OrItem =
			from _ in Pipe
			from value in  Sequence.Or(SinglePredicate)
			select value;
		public static readonly Parser<Or> Or =
			from firstPredicate in Sequence.Or(SinglePredicate)
			from otherPredicates in OrItem.AtLeastOnce()
			select new Or() { Items=new List<LexicalPredicate>(firstPredicate.AsEnumerable().Concat(otherPredicates))};




		public static readonly Parser<LexicalRule> NonAxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<LexicalPredicate>(Sequence).Or(SinglePredicate)
			select new LexicalRule() { Name = name, Predicate = predicate, IsAxiom =false };

		public static readonly Parser<LexicalRule> AxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('*')
			from __ in Parse.WhiteSpace.Many()
			from ___ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<LexicalPredicate>(Sequence).Or(SinglePredicate)
			select new LexicalRule() { Name = name, Predicate = predicate,IsAxiom=true };

		public static readonly Parser<LexicalRule> Rule =
			AxiomRule.Or(NonAxiomRule);

	}
}
