using FSMLib.Common;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.SyntaxicAnalysis.Rules;
using FSMLib.Predicates;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.Helpers
{
    public class RuleGrammar
    {
		private static readonly Parser<char> OpenBrace = Parse.Char('{');
		private static readonly Parser<char> CloseBrace = Parse.Char('}');
		private static readonly Parser<char> OpenBracket = Parse.Char('[');
		private static readonly Parser<char> CloseBracket = Parse.Char(']');
		private static readonly Parser<char> LowerThan = Parse.Char('<');
		private static readonly Parser<char> GreaterThan = Parse.Char('>');
		private static readonly Parser<char> BackSlash = Parse.Char('\\');
		private static readonly Parser<char> Dot = Parse.Char('.');
		private static readonly Parser<char> Plus = Parse.Char('+');
		private static readonly Parser<char> Star = Parse.Char('*');
		private static readonly Parser<char> QuestionMark = Parse.Char('?');
		private static readonly Parser<char> ExclamationMark = Parse.Char('!');
		private static readonly Parser<char> Pipe = Parse.Char('|');
		private static readonly Parser<char> SemiColon = Parse.Char(';');
		private static readonly Parser<char> Colon = Parse.Char(',');

		private static Parser<char> SpecialChar = OpenBracket.Or(CloseBracket).Or(OpenBrace).Or(CloseBrace).Or(LowerThan).Or(GreaterThan).Or(BackSlash).Or(Dot).Or(Plus).Or(Star).Or(QuestionMark).Or(ExclamationMark).Or(Pipe).Or(SemiColon).Or(Colon);
		private static Parser<char> NormalChar = Parse.AnyChar.Except(SpecialChar);
		private static readonly Parser<char> EscapedChar =
			from _ in BackSlash
			from c in Parse.AnyChar
			select c;

		private static Parser<Token> SingleToken =
			from _ in LowerThan
			from Class in (NormalChar.Or(EscapedChar)).AtLeastOnce().Text().Token()
			from __ in Colon
			from Value in (NormalChar.Or(EscapedChar)).AtLeastOnce().Text().Token()
			from ___ in GreaterThan
			select new Token(Class, Value);



		public static readonly Parser<Reduce> Reduce =
			from value in SemiColon
			select new Reduce();

	
		public static readonly Parser<NonTerminal> NonTerminal =
			from open in OpenBrace
			from name in Parse.AnyChar.Except(CloseBrace).Many().Text().Token()
			from close in CloseBrace
			select new NonTerminal(name);

		public static readonly Parser<Terminal> Terminal =
			from value in SingleToken
			select new Terminal(value);


		public static readonly Parser<AnyTerminal> AnyTerminal =
			from _ in Dot
			select new AnyTerminal();

		public static readonly Parser<AnyClassTerminal> AnyClassTerminal =
			from _ in LowerThan
			from Class in (NormalChar.Or(EscapedChar).Except(GreaterThan)).AtLeastOnce().Text().Token()
			from __ in GreaterThan
			select new AnyClassTerminal(Class);

		private static readonly Parser<SyntaxicPredicate> SmallestPredicate =
			Terminal.Or<SyntaxicPredicate>(AnyTerminal).Or<SyntaxicPredicate>(AnyClassTerminal)
			.Or<SyntaxicPredicate>(NonTerminal);


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

		public static readonly Parser<SyntaxicPredicate> SinglePredicate =
			Optional.Or<SyntaxicPredicate>(ZeroOrMore).Or(OneOrMore).Or(Terminal).Or(AnyTerminal).Or(AnyClassTerminal).Or(NonTerminal);

		public static readonly Parser<Sequence> Sequence =
			from firstItem in SinglePredicate
			from items in SinglePredicate.AtLeastOnce()
			select new Sequence() { Items=new List<SyntaxicPredicate>(firstItem.AsEnumerable().Concat(items)) } ;


		private static readonly Parser<SyntaxicPredicate> OrItem =
			from _ in Pipe
			from value in  Sequence.Or(SinglePredicate)
			select value;
		public static readonly Parser<Or> Or =
			from firstPredicate in Sequence.Or(SinglePredicate)
			from otherPredicates in OrItem.AtLeastOnce()
			select new Or() { Items=new List<SyntaxicPredicate>(firstPredicate.AsEnumerable().Concat(otherPredicates))};

		public static readonly Parser<SyntaxicRule> NonAxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<SyntaxicPredicate>(Sequence).Or(SinglePredicate)
			from reducePredicate in Reduce
			select new SyntaxicRule() { Name = name, Predicate = new Sequence( predicate,reducePredicate), IsAxiom =false };

		public static readonly Parser<SyntaxicRule> AxiomRule =
			from name in Parse.Letter.Many().Text().Token()
			from _ in Parse.Char('*')
			from __ in Parse.WhiteSpace.Many()
			from ___ in Parse.Char('=')
			from predicate in RuleGrammar.Or.Or<SyntaxicPredicate>(Sequence).Or(SinglePredicate)
			from reducePredicate in Reduce
			select new SyntaxicRule() { Name = name, Predicate = new Sequence(predicate, reducePredicate), IsAxiom=true };

		public static readonly Parser<SyntaxicRule> Rule =
			AxiomRule.Or(NonAxiomRule);

	}
}
