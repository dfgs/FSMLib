using System;
using System.Linq;
using FSMLib.Automatons;
using FSMLib.ActionTables;

using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class BaseStateUnitTest
	{
		[TestMethod]
		public void ShoulEnumerateTerminals()
		{
			NonTerminalNode<char> parent, child;

			child = new NonTerminalNode<char>();
			child.States.Add(new TerminalNode<char>() { Value = 'b' });
			child.States.Add(new TerminalNode<char>() { Value = 'c' });
			child.States.Add(new TerminalNode<char>() { Value = 'd' });

			parent = new NonTerminalNode<char>();
			parent.States.Add(new TerminalNode<char>() { Value = 'a' });
			parent.States.Add(child);
			parent.States.Add(new TerminalNode<char>() { Value = 'e' });

			Assert.AreEqual("abcde", new string( parent.EnumerateTerminals().ToArray() ));
		}




	}
}
