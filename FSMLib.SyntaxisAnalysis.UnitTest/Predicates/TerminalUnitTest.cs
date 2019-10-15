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
	public class TerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Terminal predicate;

			predicate = new Terminal(new Token("C","V"));

			Assert.AreEqual("<C,V>", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Terminal predicate;

			predicate = new Terminal(new Token("C", "V"));

			Assert.AreEqual("•<C,V>", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInputs()
		{
			Terminal predicate;
			IInput<Token>[] inputs;

			predicate = new Terminal(new Token("C", "V"));
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(new Token("C", "V"), ((TerminalInput)inputs[0]).Value);
		}
		
		[TestMethod]
		public void ShouldEquals()
		{
			Terminal a,b;


			a = new Terminal(new Token("C", "V"));
			b = new Terminal(new Token("C", "V"));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Terminal a, b;


			a = new Terminal(new Token("C", "V"));
			b = new Terminal(new Token("C", "v"));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
