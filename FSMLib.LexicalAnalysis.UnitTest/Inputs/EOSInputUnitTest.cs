using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
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
			EOSInput a, b;

			a = new EOSInput();
			b = new EOSInput();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			EOSInput a;

			a = new EOSInput();

			Assert.IsFalse(a.Equals(new NonTerminalInput()));
			Assert.IsFalse(a.Equals(new LetterInput('a')));
		}

		[TestMethod]
		public void ShoudMatch()
		{
			EOSInput a,b;

			a = new EOSInput();
			b = new EOSInput();
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			EOSInput a;
			LetterInput b;

			a = new EOSInput();
			b = new LetterInput('a' );


			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(b));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			EOSInput a;

			a = new EOSInput();

			Assert.IsFalse(a.Match('a'));

		}

		[TestMethod]
		public void ShoudGetHashCode()
		{
			EOSInput a,b;

			a = new EOSInput();
			b = new EOSInput();

			Assert.AreEqual(a.GetHashCode(),b.GetHashCode());

		}

	}
}
