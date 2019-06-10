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

			predicate = new ReducePredicate<char>();

			Assert.AreEqual("←", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ReducePredicate<char> predicate;

			predicate = new ReducePredicate<char>();

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
			IInput<char>[] inputs;

			predicate = new ReducePredicate<char>();
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(0, inputs.Length);
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ReducePredicate<char> a, b;


			a = new ReducePredicate<char>();
			b = new ReducePredicate<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ReducePredicate<char> a;


			a = new ReducePredicate<char>();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal<char>()));
			Assert.IsFalse(a.Equals(new AnyTerminal<char>()));


		}




	}
}
