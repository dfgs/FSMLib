using FSMLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OneOrMoreUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			OneOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> item;

			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			Assert.AreEqual("a+", predicate.ToString());


			item = new Terminal<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			Assert.AreEqual("(abcd)+", predicate.ToString());
			predicate = new OneOrMore<char>() { Item = item };
			Assert.AreEqual("d+", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertNestedPredicateToString()
		{
			OneOrMore<char> predicate;
			Sequence<char> sequence;
			Or<char> or;
			Terminal<char> item;

			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			sequence.Items.Add(item);

			or = new Or<char>();
			item = new Terminal<char>() { Value = 'b' };
			or.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			or.Items.Add(item);

			sequence.Items.Add(or);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			Assert.AreEqual("(a(b|c)d)+", predicate.ToString());
		}

		[TestMethod]
		public void ShouldConvertComplexNestedPredicateToString()
		{
			OneOrMore<char> predicate;
			Sequence<char> parentSequence;
			Sequence<char> sequence;
			Or<char> or;
			Terminal<char> item;

			parentSequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			parentSequence.Items.Add(item);

			or = new Or<char>();
			item = new Terminal<char>() { Value = 'b' };
			or.Items.Add(item);
			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);
			or.Items.Add(sequence);

			parentSequence.Items.Add(or);
			item = new Terminal<char>() { Value = 'e' };
			parentSequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = parentSequence };
			Assert.AreEqual("(a(b|(cd))e)+", predicate.ToString());
		}


		[TestMethod]
		public void ShouldConvertToParenthesisStringWithoutBullet()
		{
			OneOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> item;

			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			Assert.AreEqual("(abcd)+", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldNotConvertToParenthesisStringWithoutBullet()
		{
			OneOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> item;

			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			Assert.AreEqual("a+", predicate.ToParenthesisString());
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			OneOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> item;
			BasePredicate<char>[] items;

			sequence = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			sequence.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			sequence.Items.Add(item);

			predicate = new OneOrMore<char>() { Item = sequence };
			items = predicate.Enumerate().ToArray();

			Assert.AreEqual(4, items.Length);
		}

		

	}
}
