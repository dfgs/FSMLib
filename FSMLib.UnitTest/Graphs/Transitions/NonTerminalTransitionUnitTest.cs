using System;
using FSMLib.Graphs.Transitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Transitions
{
	[TestClass]
	public class NonTerminalTransitionUnitTest
	{
		[TestMethod]
		public void ShouldMatchNull()
		{
			NonTerminalTransition<char> Transition;

			Transition = new NonTerminalTransition<char>();
			Assert.IsTrue(Transition.Match(null));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			NonTerminalTransition<char> Transition;

			Transition = new NonTerminalTransition<char>() { Name = "A" };
			Assert.IsTrue(Transition.Match("A"));
		}

		[TestMethod]
		public void ShouldNotMatch()
		{
			NonTerminalTransition<char> Transition;

			Transition = new NonTerminalTransition<char>() { Name = "A" };
			Assert.IsFalse(Transition.Match("B"));
		}


	}
}
