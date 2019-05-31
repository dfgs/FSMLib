using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;

namespace FSMLib.UnitTest.Inputs
{
	/// <summary>
	/// Description résumée pour TerminalInputUnitTest
	/// </summary>
	[TestClass]
	public class AnyTerminalInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			AnyTerminalInput<char> a, b;

			a = new AnyTerminalInput<char>();
			b = new AnyTerminalInput<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			AnyTerminalInput<char> a;

			a = new AnyTerminalInput<char>();

			Assert.IsFalse(a.Equals(new NonTerminalInput<char>()));
			Assert.IsFalse(a.Equals(new TerminalInput<char>()));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
			Assert.IsFalse(a.Equals(new ReduceInput<char>()));
		}

		[TestMethod]
		public void ShoudMatch()
		{
			AnyTerminalInput<char> a;
			TerminalInput<char> b;

			a = new AnyTerminalInput<char>();
			b = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(a.Match(b));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			AnyTerminalInput<char> a;
			EOSInput<char> c;

			a = new AnyTerminalInput<char>();
			c = new EOSInput<char>();

			Assert.IsFalse(a.Match(c));
			Assert.IsFalse(a.Match(null));
		}
		[TestMethod]
		public void ShoudMatchT()
		{
			AnyTerminalInput<char> a;

			a = new AnyTerminalInput<char>();
			Assert.IsTrue(a.Match('a'));

		}
		


	}
}
