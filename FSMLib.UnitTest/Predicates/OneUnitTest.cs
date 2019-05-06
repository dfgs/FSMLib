using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OneUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			One<char> predicate;

			predicate = new One<char>() { Value = 'a' };
			Assert.AreEqual("a", predicate.ToString());
			predicate = new One<char>() { Value = 'b' };
			Assert.AreEqual("b", predicate.ToString());
			predicate = new One<char>() { Value = 'c' };
			Assert.AreEqual("c", predicate.ToString());
		}
		
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBulletItem()
		{
			One<char> predicate;

			predicate = new One<char>() { Value = 'a' };
			Assert.AreEqual("a", predicate.ToParenthesisString());
			predicate = new One<char>() { Value = 'b' };
			Assert.AreEqual("b", predicate.ToParenthesisString());
			predicate = new One<char>() { Value = 'c' };
			Assert.AreEqual("c", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			One<char> predicate;
			BasePredicate<char>[] items;

			predicate = new One<char>() { Value = 'a' };
			items = predicate.Enumerate().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(predicate, items[0]);

		}

		[TestMethod]
		public void ShouldConvertImplicitelyFromValueType()
		{
			One<char> predicate;

			predicate = 'a';
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);

		}


	}
}
