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
	public class TerminalRangeInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalRangeInput<char> a, b;

			a = new TerminalRangeInput<char>() { FirstValue = 'a',LastValue='z' };
			b = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalRangeInput<char> a,b;

			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			b = new TerminalRangeInput<char>() { FirstValue = 'b', LastValue = 'z' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput<char>()));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShoudMatch()
		{
			TerminalRangeInput<char> a, b;
			TerminalInput<char> c;

			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			b = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			c = new TerminalInput<char>() { Value = 'e' };
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));
			Assert.IsTrue(a.Match(c));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			TerminalRangeInput<char> a, b;
			EOSInput<char> c;
			TerminalInput<char> d;

			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			b = new TerminalRangeInput<char>() { FirstValue = 'b', LastValue = 'z' };
			c = new EOSInput<char>();
			d = new TerminalInput<char>() { Value = 'Z' };

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));
			Assert.IsFalse(a.Match(d));

		}
		[TestMethod]
		public void ShoudMatchT()
		{
			TerminalRangeInput<char> a;

			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			Assert.IsTrue(a.Match('a'));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			TerminalRangeInput<char> a;
	
			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };

			Assert.IsFalse(a.Match('Z'));

		}
		[TestMethod]
		public void ShoudGetHashCode()
		{
			TerminalRangeInput<char> a,b;

			a = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };
			b = new TerminalRangeInput<char>() { FirstValue = 'a', LastValue = 'z' };

			Assert.AreEqual(a.GetHashCode(),b.GetHashCode());

		}

	}
}
