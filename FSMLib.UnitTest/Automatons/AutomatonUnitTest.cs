using System;
using FSMLib.Tables;
using FSMLib.Table;

using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;

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
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);
	
			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
		}
		

		[TestMethod]
		public void ShouldReset()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Reset();
			Assert.AreEqual(0, automaton.StackCount);
			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
		}
		

		[TestMethod]
		public void MayNotFeed()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			Assert.AreEqual(1, automaton.StackCount);

			Assert.ThrowsException<AutomatonException<char>>(() => automaton.Feed('z'));
			Assert.AreEqual(1, automaton.StackCount);

			automaton.Feed('b');
			Assert.AreEqual(2, automaton.StackCount);

			Assert.ThrowsException<AutomatonException<char>>(() => automaton.Feed('z'));
			Assert.AreEqual(2, automaton.StackCount);

			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.ThrowsException<AutomatonException<char>>(() => automaton.Feed('z'));
			Assert.AreEqual(3, automaton.StackCount);
		}

		[TestMethod]
		public void ShouldReturnCanAccept()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('a');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('b');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
		}
		[TestMethod]
		public void ShouldAccept()
		{
			Automaton<char> automaton;
			NonTerminalNode<char> node;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanAccept());
			node=automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


			Assert.AreEqual(3, node.Nodes.Count);
			Assert.AreEqual(2, automaton.StackCount);
			// ensure that child order is correct
			Assert.IsTrue(((TerminalNode<char>)node.Nodes[0]).Input.Match('a'));
			Assert.IsTrue(((TerminalNode<char>)node.Nodes[1]).Input.Match('b'));
			Assert.IsTrue(((TerminalNode<char>)node.Nodes[2]).Input.Match('c'));

		}
		[TestMethod]
		public void MayNotAccept()
		{
			Automaton<char> automaton;
			NonTerminalNode<char> node;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			Assert.IsFalse(automaton.CanAccept());
			Assert.ThrowsException<InvalidOperationException>(()=>automaton.Accept());
			automaton.Feed('a');
			Assert.IsFalse(automaton.CanAccept());
			Assert.ThrowsException<InvalidOperationException>(() => automaton.Accept());
			automaton.Feed('b');
			Assert.IsFalse(automaton.CanAccept());
			Assert.ThrowsException<InvalidOperationException>(() => automaton.Accept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			node = automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
			Assert.AreEqual("A", node.Name);
			Assert.AreEqual(3, node.Nodes.Count);
		}


		[TestMethod]
		public void ShouldFeedAndReduce()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable( new String[] { "A=a{BCD}e", "BCD=b{C}d", "C=c" },new char[] { 'a', 'b', 'c', 'd', 'e' });
			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsFalse(automaton.CanAccept());

			automaton.Feed('d');
			Assert.IsFalse(automaton.CanAccept());

			automaton.Feed('e');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
		}

		[TestMethod]
		public void ShouldFeedAndRecursiveReduce()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A=ab{B}d", "B={C}", "C={D}","D=c" }, new char[] { 'a', 'b', 'c', 'd', 'e' });
			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsFalse(automaton.CanAccept());

			automaton.Feed('d');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
		}

		[TestMethod]
		public void ShouldFeedAndCascadeReduce()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;


			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A=a{S}a", "S={S}b", "S=c" }, new char[] { 'a', 'b', 'c', 'd', 'e' });
			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('c');
			Assert.IsFalse(automaton.CanAccept());
	
			automaton.Feed('b');
			Assert.IsFalse(automaton.CanAccept());

			automaton.Feed('a');
			Assert.IsTrue(automaton.CanAccept());

			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


		}
		[TestMethod]
		public void ShouldFeedAndReduceNestedNonTerminal()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A=a{B}c", "B={C}", "C=b" }, new char[] { 'a', 'b', 'c', 'd', 'e' });
			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


		}

		[TestMethod]
		public void ShouldFeedGreedy()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;
			NonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc*" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			node = automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
			Assert.AreEqual(6, node.Nodes.Count);
		}

		[TestMethod]
		public void ShouldFeedAndReduceGreedy()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;
			NonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=ab{C}*" ,"C=c"}, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			Assert.IsTrue(automaton.CanAccept());

			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Feed('c');
			Assert.IsTrue(automaton.CanAccept());

			node = automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());

		}

		[TestMethod]
		public void ShouldFeedAndReduceWithOptionalPredicate()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;
			NonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=ab{C}*", "C=c" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			Assert.IsTrue(automaton.CanAccept());

			node = automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());

		}

		[TestMethod]
		public void ShouldReturnCanFeed()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=ab{C}*", "C=c" }, new char[] { 'a', 'b', 'c', 'd', 'e' });

			automaton = new Automaton<char>(automatonTable);

			Assert.IsTrue(automaton.CanFeed('a'));
			automaton.Feed('a');
			Assert.IsFalse(automaton.CanFeed('a'));
			Assert.IsTrue(automaton.CanFeed('b'));
			automaton.Feed('b');

			Assert.IsTrue(automaton.CanFeed('c'));
			automaton.Feed('c');

			Assert.IsTrue(automaton.CanFeed('c'));
			automaton.Feed('c');

			Assert.IsTrue(automaton.CanFeed('c'));
			automaton.Feed('c');

		}
		[TestMethod]
		public void ShouldRecursiveAccept()
		{
			Automaton<char> automaton;
			AutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A=ab{B}", "B={C}", "C={D}", "D=c" }, new char[] { 'a', 'b', 'c', 'd', 'e' });
			automaton = new Automaton<char>(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());
		}



	}
}
