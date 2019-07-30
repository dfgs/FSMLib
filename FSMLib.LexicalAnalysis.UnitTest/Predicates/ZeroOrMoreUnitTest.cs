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
			Letter terminal;

			terminal= new Letter( 'a' );
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("a*", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Letter( 'a' ));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter( 'a' ));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)*", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ZeroOrMore predicate;
			Sequence sequence;
			Letter terminal;

			terminal = new Letter( 'a' );
			predicate = new ZeroOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("•a*", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Letter( 'a' ));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter( 'a' ));
			predicate = new ZeroOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)*", predicate.ToString(terminal));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Letter('a');
			b = new ZeroOrMore();
			b.Item = new Letter( 'a' );

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ZeroOrMore a, b;


			a = new ZeroOrMore();
			a.Item = new Letter( 'a' );
			b = new ZeroOrMore();
			b.Item = new Letter( 'b' );

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOS()));


		}


	}
}
