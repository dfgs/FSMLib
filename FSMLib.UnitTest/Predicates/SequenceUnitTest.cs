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
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("abcd", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertNestedPredicateToString()
		{
			Sequence<char> predicate;
			Or<char> or;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);

			or = new Or<char>();
			item = new Terminal<char>() { Value = 'b' };
			or.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			or.Items.Add(item);

			predicate.Items.Add(or);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("a(b|c)d", predicate.ToString());
		}

		[TestMethod]
		public void ShouldConvertComplexNestedPredicateToString()
		{
			Sequence<char> predicate;
			Sequence<char> sequence;
			Or<char> or;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);

			or = new Or<char>();
			item = new Terminal<char>() { Value = 'b' };
			or.Items.Add(item);
			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);
			or.Items.Add(sequence);

			predicate.Items.Add(or);
			item = new Terminal<char>() { Value = 'e' };
			predicate.Items.Add(item);

			Assert.AreEqual("a(b|(cd))e", predicate.ToString());
		}


		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBullet()
		{
			Sequence<char> predicate;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			Assert.AreEqual("(abcd)", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldNotConvertToParenthesisStringWithoutBullet()
		{
			Sequence<char> predicate;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);

			Assert.AreEqual("a", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			Sequence<char> predicate;
			Terminal<char> item;
			BasePredicate<char>[] items;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			items = predicate.Enumerate().ToArray();

			Assert.AreEqual(4, items.Length);
		}

		[TestMethod]
		public void ShouldConvertImplicitelyFromPredicateArray()
		{
			Sequence<char> predicate;

			predicate = new BasePredicate<char>[] { (Terminal<char>)'a', (Terminal<char>)'b', (Terminal<char>)'c' };
			Assert.IsNotNull(predicate);
			Assert.AreEqual(3, predicate.Items.Count);

		}

		[TestMethod]
		public void ShouldConvertImplicitelyFromValueArray()
		{
			Sequence<char> predicate;

			predicate = new char[] { 'a', 'b', 'c' };
			Assert.IsNotNull(predicate);
			Assert.AreEqual(3, predicate.Items.Count);

		}

	}
}
