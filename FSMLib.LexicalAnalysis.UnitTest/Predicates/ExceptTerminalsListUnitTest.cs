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
	public class ExceptTerminalsListUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ExceptTerminalsList predicate;

			predicate = new ExceptTerminalsList( 'a','c');

			Assert.AreEqual("![a,c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ExceptTerminalsList predicate;

			predicate = new ExceptTerminalsList('a', 'c');

			Assert.AreEqual("•![a,c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInputs()
		{
			ExceptTerminalsList predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsList('b', 'd');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual(char.MinValue, ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('a', ((TerminalRangeInput)inputs[0]).LastValue);
			Assert.AreEqual('c', ((TerminalRangeInput)inputs[1]).FirstValue);
			Assert.AreEqual('c', ((TerminalRangeInput)inputs[1]).LastValue);
			Assert.AreEqual('e', ((TerminalRangeInput)inputs[2]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((TerminalRangeInput)inputs[2]).LastValue);
		}
		[TestMethod]
		public void ShouldGetInputsWithNoExclusion()
		{
			ExceptTerminalsList predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsList();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((TerminalRangeInput)inputs[0]).LastValue);
		}

		[TestMethod]
		public void ShouldGetInputsUsingFirstCharValue()
		{
			ExceptTerminalsList predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsList(char.MinValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual((char)1, ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual(char.MaxValue, ((TerminalRangeInput)inputs[0]).LastValue);

		}
		[TestMethod]
		public void ShouldGetInputsUsingLastCharValue()
		{
			ExceptTerminalsList predicate;
			IInput<char>[] inputs;

			predicate = new ExceptTerminalsList(char.MaxValue);
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(char.MinValue, ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual((char)65534, ((TerminalRangeInput)inputs[0]).LastValue);

		}
		

		[TestMethod]
		public void ShouldEquals()
		{
			ExceptTerminalsList a,b;


			a = new ExceptTerminalsList('a', 'c');
			b = new ExceptTerminalsList('c', 'a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ExceptTerminalsList a, b;


			a = new ExceptTerminalsList('a', 'c');
			b = new ExceptTerminalsList('b', 'c');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
