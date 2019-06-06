using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class ReducePredicateUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ReducePredicate<char> predicate;

			predicate = ReducePredicate<char>.Instance;

			Assert.AreEqual("←", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ReducePredicate<char> predicate;

			predicate = ReducePredicate<char>.Instance;

			Assert.AreEqual("•←", predicate.ToString(predicate));
		}

		/*[TestMethod]
		public void ShouldGetInput()
		{
			ReducePredicate<char> predicate;
			ReduceInput<char> input;

			predicate = ReducePredicate<char>.Instance;
			input = predicate.GetInput() as ReduceInput<char>;
			Assert.IsNotNull(input);
		}*/

		[TestMethod]
		public void ShouldNotGetInput()
		{
			ReducePredicate<char> predicate;
			BaseInput<char>[] inputs;

			predicate = ReducePredicate<char>.Instance;
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(0, inputs.Length);
		}



	}
}
