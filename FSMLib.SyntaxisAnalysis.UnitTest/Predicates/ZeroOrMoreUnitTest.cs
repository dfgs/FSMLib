using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.SyntaxicAnalysis.Rules;
using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class ZeroOrMoreUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ZeroOrMore predicate;
			Sequence sequence;
			Terminal terminal;

			terminal= new Terminal(new Token("C","a"));
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("<C,a>*", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Terminal(new Token("C","a")));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal(new Token("C","a")));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(<C,a><C,a><C,a>)*", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ZeroOrMore predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("•<C,a>*", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Terminal(new Token("C","a")));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal(new Token("C","a")));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(<C,a>•<C,a><C,a>)*", predicate.ToString(terminal));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Terminal(new Token("C","a"));
			b = new ZeroOrMore();
			b.Item = new Terminal(new Token("C","a"));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Terminal(new Token("C","a"));
			b = new ZeroOrMore();
			b.Item = new Terminal(new Token("C", "b"));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}


	}
}
