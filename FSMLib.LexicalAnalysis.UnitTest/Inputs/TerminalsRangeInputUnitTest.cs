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
	public class TerminalsRangeInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalsRangeInput a, b;

			a = new TerminalsRangeInput('a','z');
			b = new TerminalsRangeInput('a', 'z');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalsRangeInput a,b;

			a = new TerminalsRangeInput('a', 'z');
			b = new TerminalsRangeInput('b', 'z');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShouldMatch()
		{
			TerminalsRangeInput input;


			input = new TerminalsRangeInput('a', 'c');

			Assert.IsTrue(input.Match('a'));
			Assert.IsTrue(input.Match('b'));
			Assert.IsTrue(input.Match('c'));
			Assert.IsTrue(input.Match(new TerminalInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalsRangeInput input;


			input = new TerminalsRangeInput('a', 'c');

			Assert.IsFalse(input.Match('d'));
			Assert.IsFalse(input.Match(new TerminalInput('d')));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<char>()));

		}


	}
}
