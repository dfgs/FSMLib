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
			BaseInput<char>[] inputs;

			predicate = new EOS<char>();
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(EOSInput<char>));
		}







	}
}
