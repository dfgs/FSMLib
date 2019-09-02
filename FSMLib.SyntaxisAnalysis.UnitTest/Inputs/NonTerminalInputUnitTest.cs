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
	/// <summary>
	/// Description résumée pour TerminalInputUnitTest
	/// </summary>
	[TestClass]
	public class NonTerminalInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			NonTerminalInput a, b;

			a = new NonTerminalInput( "A" );
			b = new NonTerminalInput( "A" );

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			NonTerminalInput a, b;

			a = new NonTerminalInput("A" );
			b = new NonTerminalInput( "B" );

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new TerminalInput(new Token("C","V"))));
			Assert.IsFalse(a.Equals(new EOSInput<Token>()));
		}
		[TestMethod]
		public void ShouldMatch()
		{
			NonTerminalInput input;


			input = new NonTerminalInput( "A" );

			Assert.IsTrue(input.Match(new NonTerminalInput("A")));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			NonTerminalInput input;


			input = new NonTerminalInput( "A" );

			Assert.IsFalse(input.Match(new Token("C", "V")));
			Assert.IsFalse(input.Match(new TerminalInput(new Token("C", "V"))));
			Assert.IsFalse(input.Match(new NonTerminalInput("B")));
			Assert.IsFalse(input.Match(new EOSInput<Token>()));

		}


	}
}
