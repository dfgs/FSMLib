using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxisAnalysis;
using FSMLib.Common.Inputs;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Inputs
{
	
	[TestClass]
	public class TerminalInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalInput a, b;

			a = new TerminalInput(new Token("C","V") );
			b = new TerminalInput(new Token("C", "V"));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalInput a,b;

			a = new TerminalInput(new Token("C", "V"));
			b = new TerminalInput(new Token("C", "v"));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			TerminalInput input;


			input = new TerminalInput(new Token("C", "V"));

			Assert.IsTrue(input.Match(new Token("C", "V")));
			Assert.IsTrue(input.Match(new TerminalInput(new Token("C", "V"))));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalInput input;


			input = new TerminalInput(new Token("C", "V"));

			Assert.IsFalse(input.Match(new Token("C", "v")));
			Assert.IsFalse(input.Match(new TerminalInput(new Token("C", "v"))));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<Token>()));

		}

	}
}
