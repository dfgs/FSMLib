using FSMLib.Inputs;
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
			EOS<char> predicate;

			predicate = new EOS<char>();

			Assert.AreEqual("¤", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			EOS<char> predicate;

			predicate = new EOS<char>();

			Assert.AreEqual("•¤", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldGetInput()
		{
			EOS<char> predicate;
			IInput<char>[] inputs;

			predicate = new EOS<char>();
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(EOSInput<char>));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			EOS<char> a, b;


			a = new EOS<char>();
			b = new EOS<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			EOS<char> a;


			a = new EOS<char>();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal<char>()));
			Assert.IsFalse(a.Equals(new AnyTerminal<char>()));


		}




	}
}
