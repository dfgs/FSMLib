using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class TerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Terminal<char> predicate;

			predicate = new Terminal<char>() { Value = 'a' };
			Assert.AreEqual("a", predicate.ToString());
			predicate = new Terminal<char>() { Value = 'b' };
			Assert.AreEqual("b", predicate.ToString());
			predicate = new Terminal<char>() { Value = 'c' };
			Assert.AreEqual("c", predicate.ToString());
		}
		
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBulletItem()
		{
			Terminal<char> predicate;

			predicate = new Terminal<char>() { Value = 'a' };
			Assert.AreEqual("a", predicate.ToParenthesisString());
			predicate = new Terminal<char>() { Value = 'b' };
			Assert.AreEqual("b", predicate.ToParenthesisString());
			predicate = new Terminal<char>() { Value = 'c' };
			Assert.AreEqual("c", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			Terminal<char> predicate;
			BasePredicate<char>[] items;

			predicate = new Terminal<char>() { Value = 'a' };
			items = predicate.Enumerate().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(predicate, items[0]);

		}

		[TestMethod]
		public void ShouldConvertImplicitelyFromValueType()
		{
			Terminal<char> predicate;

			predicate = 'a';
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);

		}


	}
}
