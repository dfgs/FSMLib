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
			IInput<char> input;

			predicate = new NonTerminal<char>() { Name="A" } ;
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.AreEqual("A", ((NonTerminalInput<char>)input).Name);
		}

		[TestMethod]
		public void ShouldMatch()
		{
			NonTerminal<char> predicate;


			predicate = new NonTerminal<char>() { Name = "A" };

			Assert.IsTrue(predicate.Match(new NonTerminalInput<char>() { Name = "A" }));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			NonTerminal<char> predicate;


			predicate = new NonTerminal<char>() { Name = "A" };

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "B" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>()));

		}

		[TestMethod]
		public void ShouldEquals()
		{
			NonTerminal<char> a, b;


			a = new NonTerminal<char>() { Name="A" };
			b = new NonTerminal<char>() { Name = "A" };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			NonTerminal<char> a, b;


			a = new NonTerminal<char>() { Name = "A" };
			b = new NonTerminal<char>() { Name = "B" };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal<char>()));
			Assert.IsFalse(b.Equals(new EOS<char>()));


		}





	}
}
