﻿using System;
using FSMLib.Actions;
using FSMLib.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShiftUnitTest
	{
		

		/*[TestMethod]
		public void ShouldMatch()
		{
			Shift<char> Action;

			Action = new Shift<char>() { Input = 'a' };
			Assert.AreEqual('a',Action.Input.Value);
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			Shift<char> Action;

			Action = new Shift<char>() { Input = 'a' };
			Assert.IsFalse(Action.Match('b'));
		}*/

		[TestMethod]
		public void ShouldEquals()
		{
			Shift<char> a, b,c,d;

			a = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			b = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			c = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput<char>() { Name = "a" } };
			d = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput<char>() { Name = "a" } };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
			Assert.IsTrue(c.Equals(d));
			Assert.IsTrue(d.Equals(c));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Shift<char> a, b,c,d;

			a = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'a' } };
			b = new Shift<char>() { TargetStateIndex = 1, Input = new TerminalInput<char>() { Value = 'b' } };
			c = new Shift<char>() { TargetStateIndex = 2, Input = new TerminalInput<char>() { Value = 'a' } };
			d = new Shift<char>() { TargetStateIndex = 2, Input = new NonTerminalInput<char>() { Name = "a" } };

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