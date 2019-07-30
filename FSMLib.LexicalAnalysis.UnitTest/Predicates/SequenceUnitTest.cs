using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class SequenceUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Sequence predicate;
			Letter terminal;

			terminal = new Letter('a');
			predicate = new Sequence();
			predicate.Items.Add(new Letter('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Letter('a'));
			Assert.AreEqual("(aaa)", predicate.ToString());

			predicate = new Sequence();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Sequence predicate;
			Letter terminal;

			terminal = new Letter('a');
			predicate = new Sequence();
			predicate.Items.Add(new Letter('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Letter('a'));
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
			a.Items.Add(new Letter('a'));
			a.Items.Add(new Letter('b'));
			a.Items.Add(new Letter('c'));
			b = new Sequence();
			b.Items.Add(new Letter('a'));
			b.Items.Add(new Letter('b'));
			b.Items.Add(new Letter('c'));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Sequence a, b;


			a = new Sequence();
			a.Items.Add(new Letter('a'));
			a.Items.Add(new Letter('b'));
			a.Items.Add(new Letter('c'));
			b = new Sequence();
			b.Items.Add(new Letter('a'));
			b.Items.Add(new Letter('c'));
			b.Items.Add(new Letter('b'));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
