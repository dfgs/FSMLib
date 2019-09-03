using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class ReducePredicateUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Reduce predicate;

			predicate = new Reduce();

			Assert.AreEqual(";", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Reduce predicate;

			predicate = new Reduce();

			Assert.AreEqual("•;", predicate.ToString(predicate));
		}

		


		[TestMethod]
		public void ShouldGetInputs()
		{
			Reduce predicate;
			IInput<Token>[] inputs;

			predicate = new Reduce();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(EOSInput<Token>));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			Reduce a, b;


			a = new Reduce();
			b = new Reduce();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Reduce a;


			a = new Reduce();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal(new Token("C","a"))));
			Assert.IsFalse(a.Equals(new AnyTerminal()));


		}




	}
}
