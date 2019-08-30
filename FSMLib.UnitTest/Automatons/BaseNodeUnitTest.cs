using System;
using System.Linq;
using FSMLib.Table;

using FSMLib.Helpers;
using FSMLib.Predicates;

using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.Automatons;
using FSMLib.LexicalAnalysis.Inputs;

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
			child.Nodes.Add(new TerminalNode<char>(new TerminalInput( 'b' ) ));
			child.Nodes.Add(new TerminalNode<char>(new TerminalInput( 'c' ) ));
			child.Nodes.Add(new TerminalNode<char>(new TerminalInput( 'd' ) ));

			parent = new NonTerminalNode<char>();
			parent.Nodes.Add(new TerminalNode<char>( new TerminalInput('a' ) ));
			parent.Nodes.Add(child);
			parent.Nodes.Add(new TerminalNode<char>(new TerminalInput('e' ) ));

			Assert.AreEqual("abcde", new string( parent.EnumerateInputs().OfType<TerminalInput>().Select(item => item.Value).ToArray() ));
		}




	}
}
