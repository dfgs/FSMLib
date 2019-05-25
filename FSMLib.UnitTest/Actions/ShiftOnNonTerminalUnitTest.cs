using System;
using FSMLib.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShiftOnNonTerminalUnitTest
	{
		/*[TestMethod]
		public void ShouldMatchNull()
		{
			ShiftOnNonTerminal<char> Action;

			Action = new ShiftOnNonTerminal<char>();
			Assert.IsTrue(Action.Match(null));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			ShiftOnNonTerminal<char> Action;

			Action = new ShiftOnNonTerminal<char>() { Name = "A" };
			Assert.IsTrue(Action.Match("A"));
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			ShiftOnNonTerminal<char> Action;

			Action = new ShiftOnNonTerminal<char>() { Name = "A" };
			Assert.IsFalse(Action.Match("B"));
		}*/

		[TestMethod]
		public void ShouldEquals()
		{
			ShiftOnNonTerminal<char> a, b;

			a = new ShiftOnNonTerminal<char>() { TargetStateIndex = 1, Name = "A" };
			b = new ShiftOnNonTerminal<char>() { TargetStateIndex = 1, Name = "A" };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			ShiftOnNonTerminal<char> a, b, c;

			a = new ShiftOnNonTerminal<char>() { TargetStateIndex = 1, Name = "A" };
			b = new ShiftOnNonTerminal<char>() { TargetStateIndex = 1, Name = "B" };
			c = new ShiftOnNonTerminal<char>() { TargetStateIndex = 2, Name = "A"};

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
			Assert.IsFalse(a.Equals(c));
			Assert.IsFalse(b.Equals(c));
		}




	}
}
