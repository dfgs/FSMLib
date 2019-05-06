using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class AnyTerminalInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			AnyTerminalInput<char> input;

			input = new AnyTerminalInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchTerminalInput()
		{
			AnyTerminalInput<char> input;
			IInput<char> other;

			input = new AnyTerminalInput<char>();
			other = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('a'));
		}



		[TestMethod]
		public void ShouldMatchAnyTerminalInput()
		{
			AnyTerminalInput<char> input;
			IInput<char> other;

			input = new AnyTerminalInput<char>();
			other = new AnyTerminalInput<char>();
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('b'));
		}


		[TestMethod]
		public void ShouldNotMatchRuleInput()
		{
			AnyTerminalInput<char> input;
			IInput<char> other;

			input = new AnyTerminalInput<char>();
			other = new NonTerminalInput<char>() { Name = "A" };
			Assert.IsFalse(input.Match(other));
		}

		[TestMethod]
		public void ShouldBeEqual()
		{
			AnyTerminalInput<char> a, b;

			a = new AnyTerminalInput<char>() ;
			b = new AnyTerminalInput<char>() ;
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEqual()
		{
			AnyTerminalInput<char> a;
			TerminalInput<char> c;

			a = new AnyTerminalInput<char>() ;
			c = new TerminalInput<char>();
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(c));
		}

		[TestMethod]
		public void ShouldConvertToString()
		{
			AnyTerminalInput<char> a;
			a = new AnyTerminalInput<char>();
			Assert.AreEqual(".", a.ToString());
		}



	}

}
