using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyTerminal<char> predicate;

			predicate = new AnyTerminal<char>();
			Assert.AreEqual(".", predicate.ToString());
		
		}
		
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBulletItem()
		{
			AnyTerminal<char> predicate;

			predicate = new AnyTerminal<char>();
			Assert.AreEqual(".", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			AnyTerminal<char> predicate;
			BasePredicate<char>[] items;

			predicate = new AnyTerminal<char>();
			items = predicate.Enumerate().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(predicate, items[0]);

		}

		


	}
}
