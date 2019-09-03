using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxisAnalysis;

namespace FFSMLib.SyntaxicAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class OrUnitTest
	{


		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Or predicate;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Or();
			predicate.Items.Add(new Terminal(new Token("C","a")));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal(new Token("C","a")));
			Assert.AreEqual("(<C,a>|<C,a>|<C,a>)", predicate.ToString());

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("<C,a>", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Or predicate;
			Terminal terminal;

			terminal = new Terminal(new Token("C","a"));
			predicate = new Or();
			predicate.Items.Add(new Terminal(new Token("C","a")));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal(new Token("C","a")));
			Assert.AreEqual("(<C,a>|•<C,a>|<C,a>)", predicate.ToString(terminal));

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•<C,a>", predicate.ToString(terminal));
		}




		


		[TestMethod]
		public void ShouldEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Terminal(new Token("C","a")));
			a.Items.Add(new Terminal(new Token("C", "b")));
			a.Items.Add(new Terminal(new Token("C", "c")));
			b = new Or();
			b.Items.Add(new Terminal(new Token("C","a")));
			b.Items.Add(new Terminal(new Token("C", "b")));
			b.Items.Add(new Terminal(new Token("C", "c")));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Terminal(new Token("C","a")));
			a.Items.Add(new Terminal(new Token("C", "b")));
			a.Items.Add(new Terminal(new Token("C", "c")));
			b = new Or();
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
