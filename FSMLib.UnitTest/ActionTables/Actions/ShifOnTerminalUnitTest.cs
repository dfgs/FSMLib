using System;
using FSMLib.ActionTables.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.ActionTables.Actions
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


	}
}
