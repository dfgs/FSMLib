using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
{
	/// <summary>
	/// Description résumée pour LetterInputUnitTest
	/// </summary>
	[TestClass]
	public class TerminalRangeInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalRangeInput a, b;

			a = new TerminalRangeInput('a','z');
			b = new TerminalRangeInput('a', 'z');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalRangeInput a,b;

			a = new TerminalRangeInput('a', 'z');
			b = new TerminalRangeInput('b', 'z');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShouldMatch()
		{
			TerminalRangeInput input;


			input = new TerminalRangeInput('a', 'c');

			Assert.IsTrue(input.Match('a'));
			Assert.IsTrue(input.Match('b'));
			Assert.IsTrue(input.Match('c'));
			Assert.IsTrue(input.Match(new TerminalInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalRangeInput input;


			input = new TerminalRangeInput('a', 'c');

			Assert.IsFalse(input.Match('d'));
			Assert.IsFalse(input.Match(new TerminalInput('d')));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<char>()));

		}


	}
}
