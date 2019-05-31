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
	public class ReduceInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			ReduceInput<char> a, b;

			a = new ReduceInput<char>();
			b = new ReduceInput<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			ReduceInput<char> a;

			a = new ReduceInput<char>();

			Assert.IsFalse(a.Equals(new NonTerminalInput<char>()));
			Assert.IsFalse(a.Equals(new TerminalInput<char>()));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
			Assert.IsFalse(a.Equals(new AnyTerminalInput<char>()));
		}

		
		[TestMethod]
		public void ShoudNotMatch()
		{
			ReduceInput<char> a;
			EOSInput<char> c;
			ReduceInput<char> d;

			a = new ReduceInput<char>();
			c = new EOSInput<char>();
			d = new ReduceInput<char>();

			Assert.IsFalse(a.Match(c));
			Assert.IsFalse(a.Match(d));
			Assert.IsFalse(a.Match(null));
		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			ReduceInput<char> a;

			a = new ReduceInput<char>();
			Assert.IsFalse(a.Match('a'));

		}
		


	}
}
