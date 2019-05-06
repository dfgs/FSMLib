﻿using System;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class AutomatonUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Automaton<char>(null));
		}

		[TestMethod]
		public void ShouldFeed()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);
		}
		[TestMethod]
		public void ShouldReset()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());

			Assert.IsTrue(automaton.Feed('a'));
			automaton.Reset();
			Assert.AreEqual(0, automaton.StackCount);
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
		}

		[TestMethod]
		public void MayNotFeed()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());

			Assert.IsTrue(automaton.Feed('a'));
			Assert.AreEqual(1, automaton.StackCount);
			Assert.IsFalse(automaton.Feed('z'));
			Assert.AreEqual(1, automaton.StackCount);
			Assert.IsTrue(automaton.Feed('b'));
			Assert.AreEqual(2, automaton.StackCount);
			Assert.IsFalse(automaton.Feed('z'));
			Assert.AreEqual(2, automaton.StackCount);
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsFalse(automaton.Feed('z'));
			Assert.AreEqual(3, automaton.StackCount);
		}
		[TestMethod]
		public void ShouldReturnCanReduce()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsFalse(automaton.CanReduce());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsFalse(automaton.CanReduce());
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsFalse(automaton.CanReduce());
			Assert.IsTrue(automaton.Feed('c'));
			Assert.IsTrue(automaton.CanReduce());
		}
		[TestMethod]
		public void ShouldReduce()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);
			Assert.AreEqual("A",automaton.Reduce());
			Assert.AreEqual(0, automaton.StackCount);
		}
		[TestMethod]
		public void MayNotReduce()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual("A", automaton.Reduce());
		}


		[TestMethod]
		public void ShouldFeedUsingMostExplicitTransition()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph6());

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.AreEqual("A", automaton.Reduce());

			automaton.Reset();
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.AreEqual("B", automaton.Reduce());
		}



	}
}
