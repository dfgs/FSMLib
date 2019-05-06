using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OptionalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			One<char> item;

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			Assert.AreEqual("a?", predicate.ToString());


			item = new One<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			Assert.AreEqual("(abcd)?", predicate.ToString());
			predicate = new Optional<char>() { Item = item };
			Assert.AreEqual("d?", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertNestedPredicateToString()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			Or<char> or;
			One<char> item;

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			sequence.Items.Add(item);

			or = new Or<char>();
			item = new One<char>() { Value = 'b' };
			or.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			or.Items.Add(item);

			sequence.Items.Add(or);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			Assert.AreEqual("(a(b|c)d)?", predicate.ToString());
		}

		[TestMethod]
		public void ShouldConvertComplexNestedPredicateToString()
		{
			Optional<char> predicate;
			Sequence<char> parentSequence;
			Sequence<char> sequence;
			Or<char> or;
			One<char> item;

			parentSequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			parentSequence.Items.Add(item);

			or = new Or<char>();
			item = new One<char>() { Value = 'b' };
			or.Items.Add(item);
			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);
			or.Items.Add(sequence);

			parentSequence.Items.Add(or);
			item = new One<char>() { Value = 'e' };
			parentSequence.Items.Add(item);

			predicate = new Optional<char>() { Item = parentSequence };
			Assert.AreEqual("(a(b|(cd))e)?", predicate.ToString());
		}


		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBullet()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			One<char> item;

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			Assert.AreEqual("(abcd)?", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldNotConvertToParenthesisStringWithoutBullet()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			One<char> item;

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			Assert.AreEqual("a?", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			One<char> item;
			BasePredicate<char>[] items;

			sequence = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new Optional<char>() { Item = sequence };
			items = predicate.Enumerate().ToArray();

			Assert.AreEqual(4, items.Length);
		}

		

	}
}
