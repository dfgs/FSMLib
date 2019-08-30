using FSMLib.Common.Inputs;
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
	public class EOSUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			EOS predicate;

			predicate = new EOS();

			Assert.AreEqual("¤", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			EOS predicate;

			predicate = new EOS();

			Assert.AreEqual("•¤", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInputs()
		{
			EOS predicate;
			IInput<char>[] inputs;

			predicate = new EOS();
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(EOSInput<char>));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			EOS a, b;


			a = new EOS();
			b = new EOS();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			EOS a;


			a = new EOS();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal('a')));
			Assert.IsFalse(a.Equals(new AnyTerminal()));


		}




	}
}
