using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class ReducePredicateUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Reduce predicate;

			predicate = new Reduce();

			Assert.AreEqual("←", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Reduce predicate;

			predicate = new Reduce();

			Assert.AreEqual("•←", predicate.ToString(predicate));
		}

		


		[TestMethod]
		public void ShouldNotGetInput()
		{
			Reduce predicate;
			IInput<char> input;

			predicate = new Reduce();
			input = predicate.GetInput();
			Assert.IsNull(input);
		}

		[TestMethod]
		public void ShouldEquals()
		{
			Reduce a, b;


			a = new Reduce();
			b = new Reduce();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Reduce a;


			a = new Reduce();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Letter('a')));
			Assert.IsFalse(a.Equals(new AnyLetter()));


		}




	}
}
