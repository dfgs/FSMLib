using FSMLib.Inputs;
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

			predicate = new LettersRange() { FirstValue = 'a',LastValue='c' };

			Assert.AreEqual("[a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			LettersRange predicate;

			predicate = new LettersRange() { FirstValue = 'a', LastValue = 'c' };

			Assert.AreEqual("•[a-c]", predicate.ToString(predicate));
		}


		[TestMethod]
		public void ShouldGetInput()
		{
			LettersRange predicate;
			IInput<char> input;

			predicate = new LettersRange() { FirstValue='a',LastValue='z' };
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual('a', ((TerminalRangeInput<char>)input).FirstValue);
			Assert.AreEqual('z', ((TerminalRangeInput<char>)input).LastValue);
		}

		[TestMethod]
		public void ShouldMatch()
		{
			LettersRange predicate;


			predicate = new LettersRange() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsTrue(predicate.Match('b'));
			Assert.IsTrue(predicate.Match('c'));
			Assert.IsTrue(predicate.Match(new TerminalInput<char>() {Value='a' }));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			LettersRange predicate;


			predicate = new LettersRange() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsFalse(predicate.Match('d'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'd' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>() ));

		}
		[TestMethod]
		public void ShouldEquals()
		{
			LettersRange a,b;


			a = new LettersRange() { FirstValue = 'a', LastValue = 'c' };
			b = new LettersRange() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			LettersRange a, b;


			a = new LettersRange() { FirstValue = 'a', LastValue = 'c' };
			b = new LettersRange() { FirstValue = 'b', LastValue = 'c' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOSPredicate<char>()));


		}


	}
}
