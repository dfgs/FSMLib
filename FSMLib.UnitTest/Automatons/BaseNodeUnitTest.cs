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
			child.Nodes.Add(new TerminalNode<char>(new LetterInput( 'b' ) ));
			child.Nodes.Add(new TerminalNode<char>(new LetterInput( 'c' ) ));
			child.Nodes.Add(new TerminalNode<char>(new LetterInput( 'd' ) ));

			parent = new NonTerminalNode<char>();
			parent.Nodes.Add(new TerminalNode<char>( new LetterInput('a' ) ));
			parent.Nodes.Add(child);
			parent.Nodes.Add(new TerminalNode<char>(new LetterInput('e' ) ));

			Assert.AreEqual("abcde", new string( parent.EnumerateInputs().OfType<LetterInput>().Select(item => item.Value).ToArray() ));
		}




	}
}
