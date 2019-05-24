using System;
using System.Linq;
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
	public class BaseStateUnitTest
	{
		[TestMethod]
		public void ShoulEnumerateTerminals()
		{
			NonTerminalNode<char> parent, child;

			child = new NonTerminalNode<char>();
			child.Nodes.Add(new TerminalNode<char>() { Input = new TerminalInput<char>() { Value = 'b' } });
			child.Nodes.Add(new TerminalNode<char>() { Input = new TerminalInput<char>() { Value = 'c' } });
			child.Nodes.Add(new TerminalNode<char>() { Input = new TerminalInput<char>() { Value = 'd' } });

			parent = new NonTerminalNode<char>();
			parent.Nodes.Add(new TerminalNode<char>() { Input = new TerminalInput<char>() { Value='a' } });
			parent.Nodes.Add(child);
			parent.Nodes.Add(new TerminalNode<char>() { Input = new TerminalInput<char>() { Value = 'e' } });

			Assert.AreEqual("abcde", new string( parent.EnumerateInputs().OfType<TerminalInput<char>>().Select(item => item.Value).ToArray() ));
		}




	}
}
