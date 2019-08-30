using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
{
	
	[TestClass]
	public class TerminalInputUnitTest
	{

		[TestMethod]
		public void ShoudEquals()
		{
			TerminalInput a, b;

			a = new TerminalInput('a' );
			b = new TerminalInput('a' );

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShoudNotEquals()
		{
			TerminalInput a,b;

			a = new TerminalInput('a');
			b = new TerminalInput('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(new NonTerminalInput("A")));
			Assert.IsFalse(a.Equals(new EOSInput<char>()));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			TerminalInput input;


			input = new TerminalInput('a');

			Assert.IsTrue(input.Match('a'));
			Assert.IsTrue(input.Match(new TerminalInput('a')));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalInput input;


			input = new TerminalInput('a');

			Assert.IsFalse(input.Match('b'));
			Assert.IsFalse(input.Match(new TerminalInput('b')));
			Assert.IsFalse(input.Match(new NonTerminalInput("a")));
			Assert.IsFalse(input.Match(new EOSInput<char>()));

		}

	}
}
