using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
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

			terminal= new Terminal( 'a' );
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("a*", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Terminal( 'a' ));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal( 'a' ));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)*", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ZeroOrMore predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal( 'a' );
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("•a*", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Terminal( 'a' ));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal( 'a' ));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)*", predicate.ToString(terminal));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Terminal('a');
			b = new ZeroOrMore();
			b.Item = new Terminal( 'a' );

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Terminal( 'a' );
			b = new ZeroOrMore();
			b.Item = new Terminal( 'b' );

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}


	}
}
