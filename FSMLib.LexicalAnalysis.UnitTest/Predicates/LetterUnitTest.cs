using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class LetterUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Letter predicate;

			predicate = new Letter() { Value = 'a' };

			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Letter predicate;

			predicate = new Letter() { Value = 'a' };

			Assert.AreEqual("•a", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInput()
		{
			Letter predicate;
			IInput<char> input;

			predicate = new Letter() { Value = 'a' };
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual('a', ((TerminalInput<char>)input).Value);
		}
		[TestMethod]
		public void ShouldMatch()
		{
			Letter predicate;
			

			predicate = new Letter() { Value = 'a' };

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsTrue(predicate.Match(new TerminalInput<char>() {Value='a' }));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			Letter predicate;


			predicate = new Letter() { Value = 'a' };

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>() ));

		}
		[TestMethod]
		public void ShouldEquals()
		{
			Letter a,b;


			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'a' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Letter a, b;


			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOSPredicate<char>()));


		}


	}
}
