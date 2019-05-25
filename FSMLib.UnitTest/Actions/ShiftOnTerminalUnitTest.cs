using System;
using FSMLib.Actions;
using FSMLib.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShiftOnTerminalUnitTest
	{
		

		/*[TestMethod]
		public void ShouldMatch()
		{
			ShiftOnTerminal<char> Action;

			Action = new ShiftOnTerminal<char>() { Input = 'a' };
			Assert.AreEqual('a',Action.Input.Value);
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			ShiftOnTerminal<char> Action;

			Action = new ShiftOnTerminal<char>() { Input = 'a' };
			Assert.IsFalse(Action.Match('b'));
		}*/

		[TestMethod]
		public void ShouldEquals()
		{
			ShiftOnTerminal<char> a, b;

			a = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			b = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ShiftOnTerminal<char> a, b,c;

			a = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			b = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'b' } };
			c = new ShiftOnTerminal<char>() { TargetStateIndex = 2, Input = new TerminalInput<char>() { Value = 'a' } };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
			Assert.IsFalse(a.Equals(c));
			Assert.IsFalse(b.Equals(c));
			Assert.IsFalse(a.Equals(null));
		}


	}
}
