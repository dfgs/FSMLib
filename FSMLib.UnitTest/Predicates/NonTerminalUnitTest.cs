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
			BaseInput<char>[] inputs;

			predicate = new NonTerminal<char>() { Name="A" } ;
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(1,inputs.Length);
			Assert.AreEqual("A", ((NonTerminalInput<char>)inputs[0]).Name);
		}

	






	}
}
