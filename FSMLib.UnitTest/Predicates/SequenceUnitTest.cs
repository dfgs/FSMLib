using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
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
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Sequence<char>();
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			Assert.AreEqual("(aaa)", predicate.ToString());

			predicate = new Sequence<char>();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Sequence<char> predicate;
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Sequence<char>();
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			Assert.AreEqual("(a•aa)", predicate.ToString(terminal));

			predicate = new Sequence<char>();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
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
