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
		}

		[TestMethod]
		public void ShoudMatch()
		{
			AnyTerminalInput<char> a,b;
			TerminalInput<char> c;

			a = new AnyTerminalInput<char>();
			b = new AnyTerminalInput<char>();
			c = new TerminalInput<char>() { Value='c' };

			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));
			Assert.IsTrue(a.Match(c));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			AnyTerminalInput<char> a;
			NonTerminalInput<char> b;

			a = new AnyTerminalInput<char>();
			b = new NonTerminalInput<char>() { Name= "A" };


			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(b));

		}
		

		[TestMethod]
		public void ShoudGetHashCode()
		{
			AnyTerminalInput<char> a,b;

			a = new AnyTerminalInput<char>();
			b = new AnyTerminalInput<char>();

			Assert.AreEqual(a.GetHashCode(),b.GetHashCode());

		}

	}
}
