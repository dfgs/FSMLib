using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
{
	
	[TestClass]
	public class LetterInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			LetterInput a, b;

			a = new LetterInput('a' );
			b = new LetterInput('a' );

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			LetterInput a,b;

			a = new LetterInput('a');
			b = new LetterInput('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput()));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}


		[TestMethod]
		public void ShoudMatch()
		{
			LetterInput a,b;

			a = new LetterInput('a');
			b = new LetterInput('a');
			Assert.IsTrue(a.Match(b));
			Assert.IsTrue(b.Match(a));

		}
		[TestMethod]
		public void ShoudNotMatch()
		{
			LetterInput a, b;
			EOSInput<char> c;

			a = new LetterInput('a');
			b = new LetterInput('b');
			c = new EOSInput<char>();

			Assert.IsFalse(a.Match(b));
			Assert.IsFalse(b.Match(a));
			Assert.IsFalse(a.Match(null));
			Assert.IsFalse(a.Match(c));

		}
		[TestMethod]
		public void ShoudMatchT()
		{
			LetterInput a;

			a = new LetterInput('a');
			Assert.IsTrue(a.Match('a'));

		}
		[TestMethod]
		public void ShoudNotMatchT()
		{
			LetterInput a;
	
			a = new LetterInput('a');

			Assert.IsFalse(a.Match('b'));

		}
		[TestMethod]
		public void ShoudGetHashCode()
		{
			LetterInput a;

			a = new LetterInput('a');

			Assert.AreEqual('a'.GetHashCode(),a.GetHashCode());

		}

	}
}
