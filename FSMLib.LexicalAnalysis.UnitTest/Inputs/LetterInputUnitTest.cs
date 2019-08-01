﻿using System;
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
		public void ShouldMatch()
		{
			LetterInput input;


			input = new LetterInput('a');

			Assert.IsTrue(input.Match('a'));
			Assert.IsTrue(input.Match(new LetterInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			LetterInput input;


			input = new LetterInput('a');

			Assert.IsFalse(input.Match('b'));
			Assert.IsFalse(input.Match(new LetterInput('b')));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<char>()));

		}

	}
}
