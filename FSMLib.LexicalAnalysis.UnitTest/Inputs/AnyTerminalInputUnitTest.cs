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
	public class AnyTerminalInputUnitTest
	{


		[TestMethod]
		public void ShoudEquals()
		{
			AnyLetterInput a, b;

			a = new AnyLetterInput();
			b = new AnyLetterInput();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			AnyLetterInput a;

			a = new AnyLetterInput();

			Assert.IsFalse(a.Equals(new NonTerminalInput()));
			Assert.IsFalse(a.Equals(new LetterInput('a')));
		}

		[TestMethod]
		public void ShoudMatch()
		{
			AnyLetterInput a,b;
			LetterInput c;

			a = new AnyLetterInput();
			b = new AnyLetterInput();
			c = new LetterInput('c' );

			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));
			Assert.IsTrue(a.Match(c));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			AnyLetterInput a;
			NonTerminalInput b;

			a = new AnyLetterInput();
			b = new NonTerminalInput() { Name= "A" };


			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(b));

		}
		

		[TestMethod]
		public void ShoudGetHashCode()
		{
			AnyLetterInput a,b;

			a = new AnyLetterInput();
			b = new AnyLetterInput();

			Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
			Assert.AreEqual(a.GetHashCode(), HashCodes.AnyTerminal);

		}

	}
}
