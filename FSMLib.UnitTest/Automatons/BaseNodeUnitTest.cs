using System;
using System.Linq;
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
	public class BaseNodeUnitTest
	{
		[TestMethod]
		public void ShoulEnumerateTerminals()
		{
			NonTerminalNode<char> parent, child;

			child = new NonTerminalNode<char>();
			child.Nodes.Add(new TerminalNode<char>() { Value = 'b' });
			child.Nodes.Add(new TerminalNode<char>() { Value = 'c' });
			child.Nodes.Add(new TerminalNode<char>() { Value = 'd' });

			parent = new NonTerminalNode<char>();
			parent.Nodes.Add(new TerminalNode<char>() { Value = 'a' });
			parent.Nodes.Add(child);
			parent.Nodes.Add(new TerminalNode<char>() { Value = 'e' });

			Assert.AreEqual("abcde", new string( parent.EnumerateTerminals().ToArray() ));
		}




	}
}
