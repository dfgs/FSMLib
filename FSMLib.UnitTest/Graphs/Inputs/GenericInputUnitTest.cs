using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class GenericInputUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectPriorities()
		{
			Assert.IsTrue(new TerminalInput<char>().Priority < new AnyTerminalInput<char>().Priority);
			Assert.IsTrue(new AnyTerminalInput<char>().Priority < new NonTerminalInput<char>().Priority);
		}
	}
}
