using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
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
			IInput<char>[] inputs;

			predicate = new Reduce();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(EOSInput<char>));
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
			Assert.IsFalse(a.Equals(new Terminal('a')));
			Assert.IsFalse(a.Equals(new AnyTerminal()));


		}




	}
}
