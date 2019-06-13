using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OrUnitTest
	{


		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Or predicate;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new Or();
			predicate.Items.Add(new Letter() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Letter() { Value = 'a' });
			Assert.AreEqual("(a|a|a)", predicate.ToString());

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Or predicate;
			Letter terminal;

			terminal = new Letter() { Value = 'a' };
			predicate = new Or();
			predicate.Items.Add(new Letter() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Letter() { Value = 'a' });
			Assert.AreEqual("(a|•a|a)", predicate.ToString(terminal));

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
		}




		


		[TestMethod]
		public void ShouldEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Letter() { Value = 'a' });
			a.Items.Add(new Letter() { Value = 'b' });
			a.Items.Add(new Letter() { Value = 'c' });
			b = new Or();
			b.Items.Add(new Letter() { Value = 'a' });
			b.Items.Add(new Letter() { Value = 'b' });
			b.Items.Add(new Letter() { Value = 'c' });

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Letter() { Value = 'a' });
			a.Items.Add(new Letter() { Value = 'b' });
			a.Items.Add(new Letter() { Value = 'c' });
			b = new Or();
			b.Items.Add(new Letter() { Value = 'a' });
			b.Items.Add(new Letter() { Value = 'c' });
			b.Items.Add(new Letter() { Value = 'b' });

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyLetter()));
			Assert.IsFalse(a.Equals(new EOSPredicate<char>()));


		}

	}
}
