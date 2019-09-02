using System;
using FSMLib.Actions;
using FSMLib.Common.Actions;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.Common.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShiftUnitTest
	{
			

		[TestMethod]
		public void ShouldEquals()
		{
			Shift<char> a, b,c,d;

			a = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput('a' ) };
			b = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput('a' ) };
			c = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput("a" ) };
			d = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput("a" ) };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
			Assert.IsTrue(c.Equals(d));
			Assert.IsTrue(d.Equals(c));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Shift<char> a, b,c,d;

			a = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput( 'a' ) };
			b = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput( 'b' ) };
			c = new Shift<char>() { TargetStateIndex = 2, Input = new TerminalInput( 'a' ) };
			d = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput( "a" )};

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
			Assert.IsFalse(a.Equals(c));
			Assert.IsFalse(b.Equals(c));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(d));
			Assert.IsFalse(d.Equals(a));
		}

		
	}
}
