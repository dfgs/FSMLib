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
	public class LetterUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Letter predicate;

			predicate = new Letter('a');

			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Letter predicate;

			predicate = new Letter('a');

			Assert.AreEqual("•a", predicate.ToString(predicate));
		}

		
		[TestMethod]
		public void ShouldGetInput()
		{
			Letter predicate;
			IInput<char> input;

			predicate = new Letter('a');
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual('a', ((LetterInput)input).Value);
		}
		
		[TestMethod]
		public void ShouldEquals()
		{
			Letter a,b;


			a = new Letter('a');
			b = new Letter('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Letter a, b;


			a = new Letter('a');
			b = new Letter('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOS()));


		}


	}
}
