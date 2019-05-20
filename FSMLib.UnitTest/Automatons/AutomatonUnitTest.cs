using System;
using FSMLib.Automatons;
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
		public void ShouldNotFeedWithInvalidInput()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());

			Assert.IsTrue(automaton.Feed('a'));
			Assert.ThrowsException<ArgumentException>(() => automaton.Feed(new AnyTerminalInput<char>()));
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
			NonTerminalNode<char> node;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);
			node = automaton.Reduce();
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);
			Assert.AreEqual(0, automaton.StackCount);
		}
		[TestMethod]
		public void MayNotReduce()
		{
			Automaton<char> automaton;
			NonTerminalNode<char> node;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsNull(automaton.Reduce());
			Assert.IsTrue(automaton.Feed('c'));
			node = automaton.Reduce();
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);
		}


		[TestMethod]
		public void ShouldFeedUsingMostExplicitTransition()
		{
			Automaton<char> automaton;
			NonTerminalNode<char> node;

			automaton = new Automaton<char>(new TestGraph6());

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('a'));
			node = automaton.Reduce();
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count); 

			automaton.Reset();
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('a'));
			node = automaton.Reduce();
			Assert.AreEqual("B", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);
		}


		[TestMethod]
		public void ShouldFeedAndReduce()
		{
			Automaton<char> automaton;
			Graph<char> graph;
			NonTerminalNode<char> node;

			graph = GraphHelper.BuildDeterminiticGraph("A=a{BCD}e", "BCD=b{C}d", "C=c");
			automaton = new Automaton<char>(graph);

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsFalse(automaton.Feed('d'));
			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("C", node.Name);
			Assert.AreEqual(1, node.Nodes.Count);

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name="C" }  ));
			Assert.IsTrue(automaton.Feed('d'));

			Assert.IsFalse(automaton.Feed('e'));
			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("BCD", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "BCD" }));
			Assert.IsTrue(automaton.Feed('e'));

			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);

			Assert.AreEqual(0, automaton.StackCount);
		}

		[TestMethod]
		public void ShouldFeedAndReduce2()
		{
			Automaton<char> automaton;
			Graph<char> graph;
			NonTerminalNode<char> node;


			// using non terminal recursion
			graph = GraphHelper.BuildDeterminiticGraph("A=a{S}a", "S={S}b", "S=c");
			automaton = new Automaton<char>(graph);

			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('c'));
			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("S", node.Name);
			Assert.AreEqual(1, node.Nodes.Count);

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "S" }));
			Assert.IsTrue(automaton.Feed('b'));
			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("S", node.Name);
			Assert.AreEqual(2, node.Nodes.Count);

			Assert.IsTrue(automaton.Feed(new NonTerminalInput<char>() { Name = "S" }));
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.CanReduce());
			node = automaton.Reduce();
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);



			Assert.AreEqual(0, automaton.StackCount);
		}

		[TestMethod]
		public void ShouldSaveAndRestore()
		{
			Automaton<char> automaton;

			automaton = new Automaton<char>(new TestGraph5());
			Assert.IsTrue(automaton.Feed('a'));
			Assert.IsTrue(automaton.Feed('b'));
			automaton.SaveSituation();
			Assert.IsTrue(automaton.Feed('c'));
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanReduce());
			automaton.RestoreSituation();
			Assert.AreEqual(2, automaton.StackCount);
			Assert.IsFalse(automaton.CanReduce());

		}


	}
}
