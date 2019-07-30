using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
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

			terminal = new Letter('a');
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("a?", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Letter('a'));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter('a'));
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

			terminal = new Letter('a');
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("•a?", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Letter('a'));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Letter('a'));
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)?", predicate.ToString(terminal));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item = new Letter('a');
			b = new Optional();
			b.Item = new Letter('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item =  new Letter('a');
			b = new Optional();
			b.Item =  new Letter('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
