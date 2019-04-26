using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OrUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Or<char> predicate;
			One<char> item;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("a|b|c|d", predicate.ToString());
		}
		/*[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Or<char> predicate;
			One<char> item;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual($"{RulePredicate<char>.Bullet}a|b|c|d", predicate.ToString(predicate.Items[0]));
			Assert.AreEqual($"a|{RulePredicate<char>.Bullet}b|c|d", predicate.ToString(predicate.Items[1]));
			Assert.AreEqual($"a|b|{RulePredicate<char>.Bullet}c|d", predicate.ToString(predicate.Items[2]));
			Assert.AreEqual($"a|b|c|{RulePredicate<char>.Bullet}d", predicate.ToString(predicate.Items[3]));
		}
		[TestMethod]
		public void ShouldConvertNestedPredicateToString()
		{
			Or<char> predicate;
			Sequence<char> sequence;
			One<char> item;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);

			predicate.Items.Add(sequence);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("a|(bc)|d", predicate.ToString());
			Assert.AreEqual($"a|{RulePredicate<char>.Bullet}(bc)|d", predicate.ToString(sequence));
		}*/
		[TestMethod]
		public void ShouldConvertComplexNestedPredicateToString()
		{
			Or<char> predicate;
			Sequence<char> sequence;
			Or<char> or;
			One<char> item;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'b' };
			sequence.Items.Add(item);
			or = new Or<char>();
			item = new One<char>() { Value = 'c' };
			or.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			or.Items.Add(item);
			sequence.Items.Add(or);

			predicate.Items.Add(sequence);
			item = new One<char>() { Value = 'e' };
			predicate.Items.Add(item);

			Assert.AreEqual("a|(b(c|d))|e", predicate.ToString());
		}//*/
		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBullet()
		{
			Or<char> predicate;
			One<char> item;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("(a|b|c|d)", predicate.ToParenthesisString());
		}
		[TestMethod]
		public void ShouldNotConvertToParenthesisStringWithoutBullet()
		{
			Sequence<char> predicate;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);

			Assert.AreEqual("a", predicate.ToParenthesisString());
		}


		[TestMethod]
		public void ShouldEnumerate()
		{
			Or<char> predicate;
			One<char> item;
			RulePredicate<char>[] items;

			predicate = new Or<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			items = predicate.Enumerate().ToArray();

			Assert.AreEqual(4, items.Length);
		}

	}
}
