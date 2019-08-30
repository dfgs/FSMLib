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
	public class TerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Terminal predicate;

			predicate = new Terminal('a');

			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Terminal predicate;

			predicate = new Terminal('a');

			Assert.AreEqual("•a", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInputs()
		{
			Terminal predicate;
			IInput<char>[] inputs;

			predicate = new Terminal('a');
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('a', ((LetterInput)inputs[0]).Value);
		}
		
		[TestMethod]
		public void ShouldEquals()
		{
			Terminal a,b;


			a = new Terminal('a');
			b = new Terminal('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Terminal a, b;


			a = new Terminal('a');
			b = new Terminal('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
