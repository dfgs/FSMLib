using FSMLib.Predicates;
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
			Assert.AreEqual("A", predicate.ToString());
			predicate = new NonTerminal<char>() { Name = "B" };
			Assert.AreEqual("B", predicate.ToString());
			predicate = new NonTerminal<char>() { Name = "C" };
			Assert.AreEqual("C", predicate.ToString());
		}
		
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBulletItem()
		{
			NonTerminal<char> predicate;

			predicate = new NonTerminal<char>() { Name = "A" };
			Assert.AreEqual("A", predicate.ToParenthesisString());
			predicate = new NonTerminal<char>() { Name = "B" };
			Assert.AreEqual("B", predicate.ToParenthesisString());
			predicate = new NonTerminal<char>() { Name = "C" };
			Assert.AreEqual("C", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			NonTerminal<char> predicate;
			BasePredicate<char>[] items;

			predicate = new NonTerminal<char>() { Name = "A" };
			items = predicate.Enumerate().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(predicate, items[0]);

		}

		


	}
}
