using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyTerminalPredicate<char> predicate;

			predicate = new AnyLetter();

			Assert.AreEqual(".", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyTerminalPredicate<char> predicate;

			predicate = new AnyLetter();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}
		[TestMethod]
		public void ShouldGetInput()
		{
			AnyTerminalPredicate<char> predicate;
			IInput<char> input;

			predicate = new AnyLetter();
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.IsInstanceOfType(input, typeof(AnyTerminalInput<char>));
		}
		[TestMethod]
		public void ShouldMatch()
		{
			AnyTerminalPredicate<char> predicate;


			predicate = new AnyLetter();

			Assert.IsTrue(predicate.Match(new TerminalInput<char>() { Value = 'a' }));
			Assert.IsTrue(predicate.Match(new EOSInput<char>()));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			AnyTerminalPredicate<char> predicate;


			predicate = new AnyLetter();

			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "B" }));

		}

		



		[TestMethod]
		public void ShouldEquals()
		{
			AnyTerminalPredicate<char> a, b;


			a = new AnyLetter();
			b = new AnyLetter();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyTerminalPredicate<char> a;


			a = new AnyLetter();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Letter()));
			Assert.IsFalse(a.Equals(new EOSPredicate<char>()));


		}



	}
}
