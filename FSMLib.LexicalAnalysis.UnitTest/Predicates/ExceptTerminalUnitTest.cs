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
	public class ExceptTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ExceptTerminal predicate;

			predicate = new ExceptTerminal('a');

			Assert.AreEqual("!a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ExceptTerminal predicate;

			predicate = new ExceptTerminal('a');

			Assert.AreEqual("•!a", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInputs()
		{
			ExceptTerminal predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminal('b');
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
			ExceptTerminal predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminal(char.MinValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual((char)(char.MinValue+1), ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((LettersRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingLastCharValue()
		{
			ExceptTerminal predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminal(char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((LettersRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual((char)(char.MaxValue - 1), ((LettersRangeInput)inputs[0]).LastValue);

		}

		[TestMethod]
		public void ShouldEquals()
		{
			ExceptTerminal a,b;


			a = new ExceptTerminal('a');
			b = new ExceptTerminal('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ExceptTerminal a, b;


			a = new ExceptTerminal('a');
			b = new ExceptTerminal('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
