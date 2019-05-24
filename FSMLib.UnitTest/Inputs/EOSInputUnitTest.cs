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
	public class EOSInputUnitTest
	{
		

		
		[TestMethod]
		public void ShoudMatch()
		{
			EOSInput<char> a,b;

			a = new EOSInput<char>();
			b = new EOSInput<char>();
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			EOSInput<char> a;
			TerminalInput<char> b;

			a = new EOSInput<char>();
			b = new TerminalInput<char>() { Value = 'a' };


			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(b));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			EOSInput<char> a;

			a = new EOSInput<char>();

			Assert.IsFalse(a.Match('a'));

		}



	}
}
