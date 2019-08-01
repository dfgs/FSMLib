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
		public void ShouldMatch()
		{
			LettersRangeInput input;


			input = new LettersRangeInput('a', 'c');

			Assert.IsTrue(input.Match('a'));
			Assert.IsTrue(input.Match('b'));
			Assert.IsTrue(input.Match('c'));
			Assert.IsTrue(input.Match(new LetterInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			LettersRangeInput input;


			input = new LettersRangeInput('a', 'c');

			Assert.IsFalse(input.Match('d'));
			Assert.IsFalse(input.Match(new LetterInput('d')));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<char>()));

		}


	}
}
