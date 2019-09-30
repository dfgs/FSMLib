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
	public class TerminalsRangeUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			TerminalsRange predicate;

			predicate = new TerminalsRange( 'a','c');

			Assert.AreEqual("[a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			TerminalsRange predicate;

			predicate = new TerminalsRange('a', 'c');

			Assert.AreEqual("•[a-c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInputs()
		{
			TerminalsRange predicate;
			IInput<char>[] inputs;

			predicate = new TerminalsRange('a', 'z');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('a', ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('z', ((TerminalRangeInput)inputs[0]).LastValue);
		}

		[TestMethod]
		public void ShouldEquals()
		{
			TerminalsRange a,b;


			a = new TerminalsRange('a', 'c');
			b = new TerminalsRange('a', 'c');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			TerminalsRange a, b;


			a = new TerminalsRange('a', 'c');
			b = new TerminalsRange('b', 'c');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
