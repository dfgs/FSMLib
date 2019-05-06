using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class TerminalInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			TerminalInput<char> input;

			input = new TerminalInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchTerminalInput()
		{
			TerminalInput<char> input;
			IInput<char> other;

			input = new TerminalInput<char>() { Value = 'a' };
			other = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('a'));
		}

		[TestMethod]
		public void ShouldNotMatchTerminalInput()
		{
			TerminalInput<char> input;
			IInput<char> other;

			input = new TerminalInput<char>() { Value = 'a' };
			other = new TerminalInput<char>() { Value = 'b' };
			Assert.IsFalse(input.Match(other));
			Assert.IsFalse(input.Match('b'));
		}

		[TestMethod]
		public void ShouldNotMatchAnyTerminalInput()
		{
			TerminalInput<char> input;
			IInput<char> other;

			input = new TerminalInput<char>() { Value = 'a' };
			other = new AnyTerminalInput<char>();
			Assert.IsFalse(input.Match(other));
		}

		[TestMethod]
		public void ShouldNotMatchRuleInput()
		{
			TerminalInput<char> input;
			IInput<char> other;

			input = new TerminalInput<char>() { Value = 'a' };
			other = new NonTerminalInput<char>() { Name="A"};
			Assert.IsFalse(input.Match(other));
		}

		[TestMethod]
		public void ShouldBeEqual()
		{
			TerminalInput<char> a, b;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEqual()
		{
			TerminalInput<char> a, b;
			AnyTerminalInput<char> c;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'b' };
			c = new AnyTerminalInput<char>();
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(c));
		}
		[TestMethod]
		public void ShouldConvertToString()
		{
			TerminalInput<char> a, b;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'b' };
			Assert.AreEqual("a", a.ToString());
			Assert.AreEqual("b", b.ToString());
		}



	}
}
