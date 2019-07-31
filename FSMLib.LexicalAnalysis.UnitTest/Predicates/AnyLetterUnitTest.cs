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
	public class AnyLetterUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyLetter predicate;

			predicate = new AnyLetter();

			Assert.AreEqual(".", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyLetter predicate;

			predicate = new AnyLetter();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}
		[TestMethod]
		public void ShouldGetInput()
		{
			AnyLetter predicate;
			IInput<char> input;

			predicate = new AnyLetter();
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.IsInstanceOfType(input, typeof(AnyLetterInput));
		}
		[TestMethod]
		public void ShouldMatch()
		{
			AnyLetter predicate;


			predicate = new AnyLetter();

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsTrue(predicate.Match(new LetterInput('a')));
			Assert.IsTrue(predicate.Match(new EOSInput()));
			Assert.IsTrue(predicate.Match(new AnyLetterInput()));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			AnyLetter predicate;


			predicate = new AnyLetter();

			Assert.IsFalse(predicate.Match(new NonTerminalInput("B")));

		}

		



		[TestMethod]
		public void ShouldEquals()
		{
			AnyLetter a, b;


			a = new AnyLetter();
			b = new AnyLetter();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyLetter a;


			a = new AnyLetter();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Letter('a')));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
