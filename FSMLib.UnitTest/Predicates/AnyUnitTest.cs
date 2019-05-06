using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Any<char> predicate;

			predicate = new Any<char>();
			Assert.AreEqual(".", predicate.ToString());
		
		}
		
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBulletItem()
		{
			Any<char> predicate;

			predicate = new Any<char>();
			Assert.AreEqual(".", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			Any<char> predicate;
			BasePredicate<char>[] items;

			predicate = new Any<char>();
			items = predicate.Enumerate().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(predicate, items[0]);

		}

		


	}
}
