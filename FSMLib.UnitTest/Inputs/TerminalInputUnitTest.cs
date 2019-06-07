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
	public class TerminalInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalInput<char> a, b;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'a' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalInput<char> a,b;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'b' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput<char>()));
			Assert.IsFalse(a.Equals(new AnyTerminalInput<char>()));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
			//Assert.IsFalse(a.Equals(new ReduceInput<char>()));
		}


		[TestMethod]
		public void ShoudMatch()
		{
			TerminalInput<char> a,b;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			TerminalInput<char> a, b;
			EOSInput<char> c;

			a = new TerminalInput<char>() { Value = 'a' };
			b = new TerminalInput<char>() { Value = 'b' };
			c = new EOSInput<char>();

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));

		}
		[TestMethod]
		public void ShoudMatchT()
		{
			TerminalInput<char> a;

			a = new TerminalInput<char>() { Value = 'a' };
			Assert.IsTrue(a.Match('a'));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			TerminalInput<char> a;
	
			a = new TerminalInput<char>() { Value = 'a' };

			Assert.IsFalse(a.Match('b'));

		}
		[TestMethod]
		public void ShoudGetHashCode()
		{
			TerminalInput<char> a;

			a = new TerminalInput<char>() { Value = 'a' };

			Assert.AreEqual('a'.GetHashCode(),a.GetHashCode());

		}

	}
}
