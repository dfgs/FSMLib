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
	public class TerminalRangeUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			LettersRange predicate;

			predicate = new LettersRange( 'a','c');

			Assert.AreEqual("[a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			LettersRange predicate;

			predicate = new LettersRange('a', 'c');

			Assert.AreEqual("•[a-c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInput()
		{
			LettersRange predicate;
			IInput<char> input;

			predicate = new LettersRange('a', 'z');
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual('a', ((LettersRangeInput)input).FirstValue);
			Assert.AreEqual('z', ((LettersRangeInput)input).LastValue);
		}

		[TestMethod]
		public void ShouldMatch()
		{
			LettersRange predicate;


			predicate = new LettersRange('a', 'c');

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsTrue(predicate.Match('b'));
			Assert.IsTrue(predicate.Match('c'));
			Assert.IsTrue(predicate.Match(new LetterInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			LettersRange predicate;


			predicate = new LettersRange('a', 'c');

			Assert.IsFalse(predicate.Match('d'));
			Assert.IsFalse(predicate.Match(new LetterInput('d')));
			Assert.IsFalse(predicate.Match(new NonTerminalInput("a")));
			Assert.IsFalse(predicate.Match(new EOSInput<char>() ));

		}
		[TestMethod]
		public void ShouldEquals()
		{
			LettersRange a,b;


			a = new LettersRange('a', 'c');
			b = new LettersRange('a', 'c');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			LettersRange a, b;


			a = new LettersRange('a', 'c');
			b = new LettersRange('b', 'c');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
