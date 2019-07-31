using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
{
	/// <summary>
	/// Description résumée pour LetterInputUnitTest
	/// </summary>
	[TestClass]
	public class LettersRangeInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			LettersRangeInput a, b;

			a = new LettersRangeInput('a','z');
			b = new LettersRangeInput('a', 'z');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			LettersRangeInput a,b;

			a = new LettersRangeInput('a', 'z');
			b = new LettersRangeInput('b', 'z');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShoudMatch()
		{
			LettersRangeInput a, b;
			LetterInput c;

			a = new LettersRangeInput('a', 'z');
			b = new LettersRangeInput('a', 'z');
			c = new LetterInput('e');
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));
			Assert.IsTrue(a.Match(c));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			LettersRangeInput a, b;
			EOSInput<char> c;
			LetterInput d;

			a = new LettersRangeInput('a', 'z');
			b = new LettersRangeInput('b', 'z');
			c = new EOSInput<char>();
			d = new LetterInput( 'Z' );

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));
			Assert.IsFalse(a.Match(d));

		}
		[TestMethod]
		public void ShoudMatchT()
		{
			LettersRangeInput a;

			a = new LettersRangeInput('a', 'z');
			Assert.IsTrue(a.Match('a'));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			LettersRangeInput a;
	
			a = new LettersRangeInput('a', 'z');

			Assert.IsFalse(a.Match('Z'));

		}
		[TestMethod]
		public void ShoudGetHashCode()
		{
			LettersRangeInput a,b;

			a = new LettersRangeInput('a', 'z');
			b = new LettersRangeInput('a', 'z');

			Assert.AreEqual(a.GetHashCode(),b.GetHashCode());

		}

	}
}
