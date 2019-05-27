using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class NonTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			NonTerminal<char> predicate;

			predicate = new NonTerminal<char>() { Name = "A" };

			Assert.AreEqual("{A}", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			NonTerminal<char> predicate;

			predicate = new NonTerminal<char>() { Name = "A" };

			Assert.AreEqual("•{A}", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldGetInput()
		{
			NonTerminal<char> predicate;
			NonTerminalInput<char> input;

			predicate = new NonTerminal<char>() { Name="A" } ;
			input = predicate.GetInput() as NonTerminalInput<char>;
			Assert.IsNotNull(input);
			Assert.AreEqual("A", input.Name);
		}

	






	}
}
