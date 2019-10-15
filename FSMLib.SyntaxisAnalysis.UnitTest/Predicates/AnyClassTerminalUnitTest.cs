using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyClassTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyClassTerminal predicate;

			predicate = new AnyClassTerminal("C");

			Assert.AreEqual("<C>", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyClassTerminal predicate;

			predicate = new AnyClassTerminal("C");
			Assert.AreEqual("•<C>", predicate.ToString(predicate));
		}
		[TestMethod]
		public void ShouldGetInputs()
		{
			AnyClassTerminal predicate;
			IInput<Token>[] inputs;

			predicate = new AnyClassTerminal("C");
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(TerminalRangeInput));
		}
	





		[TestMethod]
		public void ShouldEquals()
		{
			AnyClassTerminal a, b;


			a = new AnyClassTerminal("C");
			b = new AnyClassTerminal("C");

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyClassTerminal a;


			a = new AnyClassTerminal("C");

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal(new Token("C", "V"))));
			Assert.IsFalse(a.Equals(new AnyClassTerminal("B")));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
