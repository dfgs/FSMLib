using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.UnitTest.Inputs
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
			Assert.IsFalse(a.Equals(new LetterInput('a')));
			Assert.IsFalse(a.Equals(new EOSInput()));
		}

		[TestMethod]
		public void ShoudMatch()
		{
			NonTerminalInput a,b;

			a = new NonTerminalInput( "A" );
			b = new NonTerminalInput( "A" );
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			NonTerminalInput a, b;
			EOSInput c;

			a = new NonTerminalInput( "A" );
			b = new NonTerminalInput( "B" );
			c = new EOSInput();

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));

		}

		[TestMethod]
		public void ShoudNotMatchT()
		{
			NonTerminalInput a;

			a = new NonTerminalInput( "A" );

			Assert.IsFalse(a.Match('a'));

		}

		[TestMethod]
		public void ShoudGetHashCode()
		{
			NonTerminalInput a;

			a = new NonTerminalInput( "A" );

			Assert.AreEqual("A".GetHashCode(), a.GetHashCode());

		}

	}
}
