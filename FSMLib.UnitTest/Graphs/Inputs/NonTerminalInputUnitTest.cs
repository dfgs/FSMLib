using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class NonTerminalInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			NonTerminalInput<char> input;

			input = new NonTerminalInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchRuleInput()
		{
			NonTerminalInput<char> input;
			IInput<char> other;

			input = new NonTerminalInput<char>() { Name = "A" };
			other = new NonTerminalInput<char>() { Name = "A" };
			Assert.IsTrue(input.Match(other));
		}

		[TestMethod]
		public void ShouldNotMatchRuleInput()
		{
			NonTerminalInput<char> input;
			IInput<char> other;

			input = new NonTerminalInput<char>() { Name = "A" };
			other = new NonTerminalInput<char>() { Name = "B" };
			Assert.IsFalse(input.Match(other));
			Assert.IsFalse(input.Match('A'));
		}

		[TestMethod]
		public void ShouldNotMatchAnyTerminalInput()
		{
			NonTerminalInput<char> input;
			IInput<char> other;

			input = new NonTerminalInput<char>() { Name = "A" };
			other = new AnyTerminalInput<char>();
			Assert.IsFalse(input.Match(other));
		}
		[TestMethod]
		public void ShouldBeEqual()
		{
			NonTerminalInput<char> a, b;

			a = new NonTerminalInput<char>() { Name = "A" };
			b = new NonTerminalInput<char>() { Name = "A" };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEqual()
		{
			NonTerminalInput<char> a, b;
			AnyTerminalInput<char> c;

			a = new NonTerminalInput<char>() { Name = "A" };
			b = new NonTerminalInput<char>() { Name = "B" };
			c = new AnyTerminalInput<char>();
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(c));
		}
		[TestMethod]
		public void ShouldConvertToString()
		{
			NonTerminalInput<char> a, b;

			a = new NonTerminalInput<char>() { Name = "A" };
			b = new NonTerminalInput<char>() { Name = "B" };
			Assert.AreEqual("A", a.ToString());
			Assert.AreEqual("B", b.ToString());
		}



	}
}
