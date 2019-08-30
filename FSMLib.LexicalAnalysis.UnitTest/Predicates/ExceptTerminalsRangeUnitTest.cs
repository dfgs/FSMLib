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
	public class ExceptTerminalsRangeUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ExceptTerminalsRange predicate;

			predicate = new ExceptTerminalsRange( 'a','c');

			Assert.AreEqual("![a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ExceptTerminalsRange predicate;

			predicate = new ExceptTerminalsRange('a', 'c');

			Assert.AreEqual("•![a-c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInputs()
		{
			ExceptTerminalsRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsRange('b', 'y');
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
			ExceptTerminalsRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsRange(char.MinValue,'b');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('c', ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingLastCharValue()
		{
			ExceptTerminalsRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsRange('b',char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('a', ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingFirstAndLastCharValue()
		{
			ExceptTerminalsRange predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsRange(char.MinValue,char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(0, inputs.Length);
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ExceptTerminalsRange a,b;


			a = new ExceptTerminalsRange('a', 'c');
			b = new ExceptTerminalsRange('a', 'c');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ExceptTerminalsRange a, b;


			a = new ExceptTerminalsRange('a', 'c');
			b = new ExceptTerminalsRange('b', 'c');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
