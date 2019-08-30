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
	public class ExceptLetterUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ExceptLetter predicate;

			predicate = new ExceptLetter('a');

			Assert.AreEqual("!a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ExceptLetter predicate;

			predicate = new ExceptLetter('a');

			Assert.AreEqual("•!a", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInputs()
		{
			ExceptLetter predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLetter('b');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(2, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('a', ((LettersRangeInput)inputs[0]).LastValue);
			Assert.AreEqual('c', ((LettersRangeInput)inputs[1]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[1]).LastValue);

		}

		[TestMethod]
		public void ShouldGetInputsUsingFirstCharValue()
		{
			ExceptLetter predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLetter(char.MinValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual((char)(char.MinValue+1), ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingLastCharValue()
		{
			ExceptLetter predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLetter(char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual((char)(char.MaxValue - 1), ((LettersRangeInput)inputs[0]).LastValue);

		}

		[TestMethod]
		public void ShouldEquals()
		{
			ExceptLetter a,b;


			a = new ExceptLetter('a');
			b = new ExceptLetter('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ExceptLetter a, b;


			a = new ExceptLetter('a');
			b = new ExceptLetter('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
