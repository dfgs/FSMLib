using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class OptionalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Optional predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("<C,a>?", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Terminal(new Token("C","a")));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal(new Token("C","a")));
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(<C,a><C,a><C,a>)?", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Optional predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("•<C,a>?", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Terminal(new Token("C","a")));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal(new Token("C","a")));
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(<C,a>•<C,a><C,a>)?", predicate.ToString(terminal));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item = new Terminal(new Token("C","a"));
			b = new Optional();
			b.Item = new Terminal(new Token("C","a"));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item =  new Terminal(new Token("C","a"));
			b = new Optional();
			b.Item =  new Terminal(new Token("C", "b"));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
