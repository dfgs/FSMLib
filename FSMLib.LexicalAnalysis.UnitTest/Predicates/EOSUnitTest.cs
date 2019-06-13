using FSMLib.Inputs;
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
			EOSPredicate<char> predicate;

			predicate = new EOSPredicate<char>();

			Assert.AreEqual("¤", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			EOSPredicate<char> predicate;

			predicate = new EOSPredicate<char>();

			Assert.AreEqual("•¤", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			EOSPredicate<char> predicate;


			predicate = new EOSPredicate<char>();

			Assert.IsTrue(predicate.Match(new EOSInput<char>()));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			EOSPredicate<char> predicate;


			predicate = new EOSPredicate<char>();

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));

		}
		[TestMethod]
		public void ShouldGetInput()
		{
			EOSPredicate<char> predicate;
			IInput<char> input;

			predicate = new EOSPredicate<char>();
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.IsInstanceOfType(input, typeof(EOSInput<char>));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			EOSPredicate<char> a, b;


			a = new EOSPredicate<char>();
			b = new EOSPredicate<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			EOSPredicate<char> a;


			a = new EOSPredicate<char>();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Letter()));
			Assert.IsFalse(a.Equals(new AnyLetter()));


		}




	}
}
