using FSMLib.Inputs;
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

			predicate = new NonTerminal() { Name = "A" };

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
		public void ShouldGetInput()
		{
			NonTerminal predicate;
			IInput<char> input;

			predicate = new NonTerminal() { Name="A" } ;
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual("A", ((NonTerminalInput<char>)input).Name);
		}

		[TestMethod]
		public void ShouldMatch()
		{
			NonTerminal predicate;


			predicate = new NonTerminal() { Name = "A" };

			Assert.IsTrue(predicate.Match(new NonTerminalInput<char>() { Name = "A" }));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			NonTerminal predicate;


			predicate = new NonTerminal() { Name = "A" };

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "B" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>()));

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
			Assert.IsFalse(b.Equals(new EOSPredicate<char>()));


		}





	}
}
