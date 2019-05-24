using System;
using FSMLib.ActionTables.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.ActionTables.Actions
{
	[TestClass]
	public class ShifOnNonTerminalUnitTest
	{
		[TestMethod]
		public void ShouldMatchNull()
		{
			ShifOnNonTerminal<char> Action;

			Action = new ShifOnNonTerminal<char>();
			Assert.IsTrue(Action.Match(null));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			ShifOnNonTerminal<char> Action;

			Action = new ShifOnNonTerminal<char>() { Name = "A" };
			Assert.IsTrue(Action.Match("A"));
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			ShifOnNonTerminal<char> Action;

			Action = new ShifOnNonTerminal<char>() { Name = "A" };
			Assert.IsFalse(Action.Match("B"));
		}


	}
}
