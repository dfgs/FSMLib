using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class SequenceUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Sequence predicate;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Sequence();
			predicate.Items.Add(new Terminal(new Token("C","a")));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal(new Token("C","a")));
			Assert.AreEqual("(aaa)", predicate.ToString());

			predicate = new Sequence();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Sequence predicate;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Sequence();
			predicate.Items.Add(new Terminal(new Token("C","a")));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal(new Token("C","a")));
			Assert.AreEqual("(a•aa)", predicate.ToString(terminal));

			predicate = new Sequence();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
		}


		

		[TestMethod]
		public void ShouldEquals()
		{
			Sequence a, b;


			a = new Sequence();
			a.Items.Add(new Terminal(new Token("C","a")));
			a.Items.Add(new Terminal(new Token("C", "b")));
			a.Items.Add(new Terminal(new Token("C", "c")));
			b = new Sequence();
			b.Items.Add(new Terminal(new Token("C","a")));
			b.Items.Add(new Terminal(new Token("C", "b")));
			b.Items.Add(new Terminal(new Token("C", "c")));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Sequence a, b;


			a = new Sequence();
			a.Items.Add(new Terminal(new Token("C","a")));
			a.Items.Add(new Terminal(new Token("C", "b")));
			a.Items.Add(new Terminal(new Token("C", "c")));
			b = new Sequence();
			b.Items.Add(new Terminal(new Token("C","a")));
			b.Items.Add(new Terminal(new Token("C", "c")));
			b.Items.Add(new Terminal(new Token("C", "b")));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
