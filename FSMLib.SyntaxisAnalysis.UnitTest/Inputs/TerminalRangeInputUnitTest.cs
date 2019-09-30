using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.Common.Inputs;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Inputs
{

	[TestClass]
	public class TerminalRangeInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalRangeInput a, b;

			a = new TerminalRangeInput(new Token("Class","A"), new Token("Class", "Z"));
			b = new TerminalRangeInput(new Token("Class", "A"), new Token("Class", "Z"));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalRangeInput a,b;

			a = new TerminalRangeInput(new Token("Class", "A"), new Token("Class", "Z"));
			b = new TerminalRangeInput(new Token("Class", "A"), new Token("Class", "Y"));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShouldMatch()
		{
			TerminalRangeInput input;


			input = new TerminalRangeInput(new Token("Class", "A"), new Token("Class", "C"));

			Assert.IsTrue(input.Match(new Token("Class", "A")));
			Assert.IsTrue(input.Match(new Token("Class", "B")));
			Assert.IsTrue(input.Match(new Token("Class", "C")));
			Assert.IsTrue(input.Match(new TerminalInput(new Token("Class", "A"))));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalRangeInput input;


			input = new TerminalRangeInput(new Token("Class", "A"), new Token("Class", "C"));

			Assert.IsFalse(input.Match(new Token("Class", "D")));
			Assert.IsFalse(input.Match(new TerminalInput(new Token("Class", "D"))));
			Assert.IsFalse(input.Match(new NonTerminalInput("A")));
			Assert.IsFalse(input.Match(new EOSInput<Token>()));

		}


	}
}
