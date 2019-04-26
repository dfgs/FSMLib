using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class SequenceUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Sequence<char> predicate;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("abcd", predicate.ToString());
		}
		/*[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Sequence<char> predicate;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual($"{RulePredicate<char>.Bullet}abcd", predicate.ToString(predicate.Items[0]));
			Assert.AreEqual($"a{RulePredicate<char>.Bullet}bcd", predicate.ToString(predicate.Items[1]));
			Assert.AreEqual($"ab{RulePredicate<char>.Bullet}cd", predicate.ToString(predicate.Items[2]));
			Assert.AreEqual($"abc{RulePredicate<char>.Bullet}d", predicate.ToString(predicate.Items[3]));
		}*/
		[TestMethod]
		public void ShouldConvertNestedPredicateToString()
		{
			Sequence<char> predicate;
			Or<char> or;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);

			or = new Or<char>();
			item = new One<char>() { Value = 'b' };
			or.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			or.Items.Add(item);

			predicate.Items.Add(or);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("a(b|c)d", predicate.ToString());
		}

		[TestMethod]
		public void ShouldConvertComplexNestedPredicateToString()
		{
			Sequence<char> predicate;
			Sequence<char> sequence;
			Or<char> or;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);

			or = new Or<char>();
			item = new One<char>() { Value = 'b' };
			or.Items.Add(item);
			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);
			or.Items.Add(sequence);

			predicate.Items.Add(or);
			item = new One<char>() { Value = 'e' };
			predicate.Items.Add(item);

			Assert.AreEqual("a(b|(cd))e", predicate.ToString());
		}


		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBullet()
		{
			Sequence<char> predicate;
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("(abcd)", predicate.ToParenthesisString());
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
			Sequence<char> predicate;
			One<char> item;
			RulePredicate<char>[] items;

			predicate = new Sequence<char>();
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
