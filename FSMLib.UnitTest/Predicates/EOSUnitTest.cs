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
			EOSInput<char> input;

			predicate = new EOS<char>();
			input = predicate.GetInput() as EOSInput<char>;
			Assert.IsNotNull(input);
		}

		

		



	}
}
