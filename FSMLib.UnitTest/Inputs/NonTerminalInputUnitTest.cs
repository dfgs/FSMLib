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
	public class NonTerminalInputUnitTest
	{
		

		
		[TestMethod]
		public void ShoudMatch()
		{
			NonTerminalInput<char> a,b;

			a = new NonTerminalInput<char>() { Name = "A" };
			b = new NonTerminalInput<char>() { Name = "A" };
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			NonTerminalInput<char> a, b;
			EOSInput<char> c;

			a = new NonTerminalInput<char>() { Name = "A" };
			b = new NonTerminalInput<char>() { Name = "B" };
			c = new EOSInput<char>();

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));

		}
		
		

	}
}
