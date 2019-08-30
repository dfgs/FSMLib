using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
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
			NonTerminal predicate;

			predicate = new NonTerminal( "A" );

			Assert.AreEqual("{A}", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			NonTerminal predicate;

			predicate = new NonTerminal() { Name = "A" };

			Assert.AreEqual("•{A}", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldGetInputs()
		{
			NonTerminal predicate;
			IInput<char>[] inputs;

			predicate = new NonTerminal() { Name="A" } ;
			inputs = predicate.GetInputs().ToArray();
			Assert.IsNotNull(inputs);
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual("A", ((NonTerminalInput)inputs[0]).Name);
		}

	

		[TestMethod]
		public void ShouldEquals()
		{
			NonTerminal a, b;


			a = new NonTerminal() { Name="A" };
			b = new NonTerminal() { Name = "A" };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			NonTerminal a, b;


			a = new NonTerminal() { Name = "A" };
			b = new NonTerminal() { Name = "B" };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyLetter()));
			Assert.IsFalse(b.Equals(new EOS()));


		}





	}
}
