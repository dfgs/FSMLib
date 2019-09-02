using FSMLib.Common.Automatons;
using FSMLib.LexicalAnalysis.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.Common.UnitTest
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
