using System;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
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

		[TestMethod]
		public void ShouldFeedAndReduce()
		{
			Automaton<char> automaton;
			Graph<char> graph;

			graph = GraphHelper.BuildDeterminiticGraph("A=a{BCD}e", "BCD=b{C}d", "C=c");
			automaton = new Automaton<char>(graph);

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsFalse(automaton.Feed('d'));
			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("C",automaton.Reduce());

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name="C" }  ));
			Assert.IsTrue(automaton.Feed('d'));

			Assert.IsFalse(automaton.Feed('e'));
			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("BCD", automaton.Reduce());

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "BCD" }));
			Assert.IsTrue(automaton.Feed('e'));

			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("A", automaton.Reduce());

			Assert.AreEqual(0, automaton.StackCount);
		}

		[TestMethod]
		public void ShouldFeedAndReduce2()
		{
			Automaton<char> automaton;
			Graph<char> graph;

			// using non terminal recursion
			graph = GraphHelper.BuildDeterminiticGraph("A=a{S}a", "S={S}b", "S=c");
			automaton = new Automaton<char>(graph);

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("S", automaton.Reduce());
			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "S" }));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("S", automaton.Reduce());
			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "S" }));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.CanReduce());
			Assert.AreEqual("A", automaton.Reduce());



			Assert.AreEqual(0, automaton.StackCount);
		}

	}
}
