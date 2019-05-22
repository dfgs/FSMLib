using System;
using FSMLib.Graphs.Transitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Transitions
{
	[TestClass]
	public class TerminalTransitionUnitTest
	{
		

		[TestMethod]
		public void ShouldMatch()
		{
			TerminalTransition<char> Transition;

			Transition = new TerminalTransition<char>() { Value = 'a' };
			Assert.IsTrue(Transition.Match('a'));
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalTransition<char> Transition;

			Transition = new TerminalTransition<char>() { Value = 'a' };
			Assert.IsFalse(Transition.Match('b'));
		}


	}
}
