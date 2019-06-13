using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OneOrMoreUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			OneOrMore predicate;
			Sequence sequence;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new OneOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("a+", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Letter() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter() { Value = 'a' });
			predicate = new OneOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)+", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			OneOrMore predicate;
			Sequence sequence;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new OneOrMore();
			predicate.Item = terminal;

			Assert.AreEqual("•a+", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Letter() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter() { Value = 'a' });
			predicate = new OneOrMore();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)+", predicate.ToString(terminal));
		}


		[TestMethod]
		public void ShouldEquals()
		{
			OneOrMore a, b;


			a = new OneOrMore();
			a.Item= new Letter() { Value = 'a' };
			b = new OneOrMore();
			b.Item = new Letter() { Value = 'a' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			OneOrMore a, b;


			a = new OneOrMore();
			a.Item = new Letter() { Value = 'a' };
			b = new OneOrMore();
			b.Item = new Letter() { Value = 'b' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOSPredicate<char>()));


		}



	}
}
