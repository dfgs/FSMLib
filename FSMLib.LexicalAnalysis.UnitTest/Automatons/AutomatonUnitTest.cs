using FSMLib.Automatons;
using FSMLib.Common.Automatons;
using FSMLib.LexicalAnalysis.Helpers;
using FSMLib.LexicalAnalysis.Automatons;
using FSMLib.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest
{
	[TestClass]
	public class AutomatonUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Automaton(null));
		}

		[TestMethod]
		public void ShouldFeed()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);
	
			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
		}
		

		[TestMethod]
		public void ShouldReset()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);

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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);

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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);
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
			Automaton automaton;
			INonTerminalNode<char> node;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanAccept());
			node=automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


			Assert.AreEqual(3, node.Nodes.Count());
			Assert.AreEqual(0, automaton.StackCount);
			// ensure that child order is correct
			Assert.AreEqual('a',((TerminalNode<char>)node.Nodes.ElementAt(0)).Input.Value);
			Assert.AreEqual('b', ((TerminalNode<char>)node.Nodes.ElementAt(1)).Input.Value);
			Assert.AreEqual('c', ((TerminalNode<char>)node.Nodes.ElementAt(2)).Input.Value);

		}
		[TestMethod]
		public void MayNotAccept()
		{
			Automaton automaton;
			INonTerminalNode<char> node;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc;" });

			automaton = new Automaton(automatonTable);

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
			
			Assert.AreEqual("A", node.Input.Name);
			Assert.AreEqual(3, node.Nodes.Count());
		}
		[TestMethod]
		public void MayNotAcceptUsingReduction()
		{
			Automaton automaton;
			INonTerminalNode<char> node;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A=abc;", "B=def;", "E*={A}|{B};" });

			automaton = new Automaton(automatonTable);

			Assert.IsFalse(automaton.CanAccept());
			Assert.ThrowsException<InvalidOperationException>(() => automaton.Accept());
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

			Assert.AreEqual("E", node.Input.Name);
			Assert.AreEqual(1, node.Nodes.Count());
		}


		[TestMethod]
		public void ShouldFeedAndReduce()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable( new String[] { "A*=a{BCD}e;", "BCD=b{C}d;", "C=c;" });
			automaton = new Automaton(automatonTable);

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
		public void ShouldFeedAndReduceUsingAnyTerminalPredicate()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;
			INonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "B*=abc;", "A*=a.c;" });
			automaton = new Automaton(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanAccept());
			node = automaton.Accept();
			Assert.AreEqual("B", node.Input.Name);

			automaton.Feed('a');
			automaton.Feed('c');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);
			Assert.IsTrue(automaton.CanAccept());
			node = automaton.Accept();
			Assert.AreEqual("A", node.Input.Name);


		}
		[TestMethod]
		public void ShouldFeedAndRecursiveReduce()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A*=ab{B}d;", "B={C};", "C={D};","D=c;" });
			automaton = new Automaton(automatonTable);

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
		public void ShouldFeedAndReduceLeftRecursiveRules()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;


			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A*=a{S}a;", "S={S}b;", "S=c;" });
			automaton = new Automaton(automatonTable);

			// aca
			automaton.Reset();
			automaton.Feed('a');
			automaton.Feed('c');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('a');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


			// acba
			automaton.Reset();
			automaton.Feed('a');
			automaton.Feed('c');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('b');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('a');
			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());

			// acbbba
			automaton.Reset();
			automaton.Feed('a');
			automaton.Feed('c');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('b');
			Assert.IsFalse(automaton.CanAccept());
			automaton.Feed('b');
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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A*=a{B}c;", "B={C};", "C=b;" });
			automaton = new Automaton(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			automaton.Feed('c');
			Assert.AreEqual(3, automaton.StackCount);

			Assert.IsTrue(automaton.CanAccept());
			automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());


		}
		[TestMethod]
		public void ShouldFeedAndReduceUsingTerminalRange()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A*=a{B}[c-z];", "B=b;"});
			automaton = new Automaton(automatonTable);

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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;
			INonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=abc*;" });

			automaton = new Automaton(automatonTable);

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
			Assert.AreEqual(6, node.Nodes.Count());
		}

		[TestMethod]
		public void ShouldFeedAndReduceGreedy()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;
			INonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=ab{C}*;" ,"C=c;"});

			automaton = new Automaton(automatonTable);

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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;
			INonTerminalNode<char> node;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=ab{C}*;", "C=c;" });

			automaton = new Automaton(automatonTable);

			automaton.Feed('a');
			automaton.Feed('b');
			Assert.IsTrue(automaton.CanAccept());

			node = automaton.Accept();
			Assert.IsFalse(automaton.CanAccept());

		}

		[TestMethod]
		public void ShouldReturnCanFeed()
		{
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new string[] { "A*=ab{C}*;", "C=c;" });

			automaton = new Automaton(automatonTable);

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
			Automaton automaton;
			IAutomatonTable<char> automatonTable;

			automatonTable = AutomatonTableHelper.BuildAutomatonTable(new String[] { "A*=ab{B};", "B={C};", "C={D};", "D=c;" });
			automaton = new Automaton(automatonTable);

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
