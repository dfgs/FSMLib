using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.Common.Inputs;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Inputs
{
	/// <summary>
	/// Description résumée pour TerminalInputUnitTest
	/// </summary>
	[TestClass]
	public class EOSInputUnitTest
	{


		[TestMethod]
		public void ShoudEquals()
		{
			EOSInput<char> a, b;

			a = new EOSInput<char>();
			b = new EOSInput<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			EOSInput<char> a;

			a = new EOSInput<char>();

			Assert.IsFalse(a.Equals(new MockedReduceInput()));
			Assert.IsFalse(a.Equals(new MockedTerminalInput('a')));
		}

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
			MockedTerminalInput b;

			a = new EOSInput<char>();
			b = new MockedTerminalInput('a' );


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
