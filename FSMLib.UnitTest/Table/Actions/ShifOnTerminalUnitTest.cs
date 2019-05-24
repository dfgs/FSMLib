using System;
using FSMLib.Table.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShifOnTerminalUnitTest
	{
		

		[TestMethod]
		public void ShouldMatch()
		{
			ShiftOnTerminal<char> Action;

			Action = new ShiftOnTerminal<char>() { Value = 'a' };
			Assert.IsTrue(Action.Match('a'));
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			ShiftOnTerminal<char> Action;

			Action = new ShiftOnTerminal<char>() { Value = 'a' };
			Assert.IsFalse(Action.Match('b'));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			ShiftOnTerminal<char> a, b;

			a = new ShiftOnTerminal<char>() { TargetStateIndex=1,Value='a' };
			b = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Value = 'a' };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ShiftOnTerminal<char> a, b,c;

			a = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Value = 'a' };
			b = new ShiftOnTerminal<char>() { TargetStateIndex = 1, Value = 'b' };
			c = new ShiftOnTerminal<char>() { TargetStateIndex = 2, Value = 'a' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
			Assert.IsFalse(a.Equals(c));
			Assert.IsFalse(b.Equals(c));
		}


	}
}
