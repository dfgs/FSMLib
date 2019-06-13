using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
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

		

		[TestMethod]
		public void ShouldNotMatch()
		{
			ReducePredicate<char> predicate;


			predicate = new ReducePredicate<char>();

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>() ));

		}
		[TestMethod]
		public void ShouldNotGetInput()
		{
			ReducePredicate<char> predicate;
			IInput<char> input;

			predicate = new ReducePredicate<char>();
			input = predicate.GetInput();
			Assert.IsNull(input);
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
			Assert.IsFalse(a.Equals(new Letter()));
			Assert.IsFalse(a.Equals(new AnyLetter()));


		}




	}
}
