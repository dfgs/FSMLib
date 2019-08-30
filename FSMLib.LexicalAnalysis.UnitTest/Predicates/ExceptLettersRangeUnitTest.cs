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
	public class ExceptLettersRangeUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ExceptLettersRange predicate;

			predicate = new ExceptLettersRange( 'a','c');

			Assert.AreEqual("![a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ExceptLettersRange predicate;

			predicate = new ExceptLettersRange('a', 'c');

			Assert.AreEqual("•![a-c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInputs()
		{
			ExceptLettersRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLettersRange('b', 'y');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(2, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('a', ((LettersRangeInput)inputs[0]).LastValue);
			Assert.AreEqual('z', ((LettersRangeInput)inputs[1]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[1]).LastValue);
		}

		[TestMethod]
		public void ShouldGetInputsUsingFirstCharValue()
		{
			ExceptLettersRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLettersRange(char.MinValue,'b');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('c', ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingLastCharValue()
		{
			ExceptLettersRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLettersRange('b',char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('a', ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingFirstAndLastCharValue()
		{
			ExceptLettersRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptLettersRange(char.MinValue,char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(0, inputs.Length);
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ExceptLettersRange a,b;


			a = new ExceptLettersRange('a', 'c');
			b = new ExceptLettersRange('a', 'c');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ExceptLettersRange a, b;


			a = new ExceptLettersRange('a', 'c');
			b = new ExceptLettersRange('b', 'c');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
