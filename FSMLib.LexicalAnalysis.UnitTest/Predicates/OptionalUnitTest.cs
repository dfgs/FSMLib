using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OptionalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Optional predicate;
			Sequence sequence;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("a?", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Letter() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter() { Value = 'a' });
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)?", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Optional predicate;
			Sequence sequence;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("•a?", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Letter() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter() { Value = 'a' });
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)?", predicate.ToString(terminal));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item = new Letter() { Value = 'a' };
			b = new Optional();
			b.Item = new Letter() { Value = 'a' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item =  new Letter() { Value = 'a' };
			b = new Optional();
			b.Item =  new Letter() { Value = 'b' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOSPredicate<char>()));


		}



	}
}
