using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxisAnalysis;
using FSMLib.SyntaxisAnalysis.Inputs;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyTerminal predicate;

			predicate = new AnyTerminal();

			Assert.AreEqual(".", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyTerminal predicate;

			predicate = new AnyTerminal();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}
		[TestMethod]
		public void ShouldGetInputs()
		{
			AnyTerminal predicate;
			IInput<Token>[] inputs;

			predicate = new AnyTerminal();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(AnyTerminalInput));
		}
	





		[TestMethod]
		public void ShouldEquals()
		{
			AnyTerminal a, b;


			a = new AnyTerminal();
			b = new AnyTerminal();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyTerminal a;


			a = new AnyTerminal();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal(new Token("C","V"))));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
