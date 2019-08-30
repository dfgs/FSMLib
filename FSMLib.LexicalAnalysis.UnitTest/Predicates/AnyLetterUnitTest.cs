using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyLetterUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyLetter predicate;

			predicate = new AnyLetter();

			Assert.AreEqual(".", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyLetter predicate;

			predicate = new AnyLetter();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}
		[TestMethod]
		public void ShouldGetInputs()
		{
			AnyLetter predicate;
			IInput<char>[] inputs;

			predicate = new AnyLetter();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(LettersRangeInput));
		}
	





		[TestMethod]
		public void ShouldEquals()
		{
			AnyLetter a, b;


			a = new AnyLetter();
			b = new AnyLetter();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyLetter a;


			a = new AnyLetter();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Letter('a')));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
