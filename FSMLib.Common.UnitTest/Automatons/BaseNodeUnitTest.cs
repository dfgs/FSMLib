using FSMLib.Common.Automatons;
using FSMLib.Common.UnitTest.Mocks;
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
			child.Nodes.Add(new TerminalNode<char>(new MockedTerminalInput( 'b' ) ));
			child.Nodes.Add(new TerminalNode<char>(new MockedTerminalInput( 'c' ) ));
			child.Nodes.Add(new TerminalNode<char>(new MockedTerminalInput( 'd' ) ));

			parent = new NonTerminalNode<char>();
			parent.Nodes.Add(new TerminalNode<char>( new MockedTerminalInput('a' ) ));
			parent.Nodes.Add(child);
			parent.Nodes.Add(new TerminalNode<char>(new MockedTerminalInput('e' ) ));
			
			Assert.AreEqual("abcde", new string( parent.EnumerateInputs().OfType<MockedTerminalInput>().Select(item => item.Value).ToArray() ));
		}




	}
}
