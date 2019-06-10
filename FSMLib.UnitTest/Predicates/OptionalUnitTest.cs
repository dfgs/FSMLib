using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
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
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Optional<char>();
			predicate.Item = terminal;

			Assert.AreEqual("a?", predicate.ToString());

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate = new Optional<char>();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)?", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Optional<char> predicate;
			Sequence<char> sequence;
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Optional<char>();
			predicate.Item = terminal;

			Assert.AreEqual("•a?", predicate.ToString(terminal));

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate = new Optional<char>();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)?", predicate.ToString(terminal));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			Optional<char> a, b;


			a = new Optional<char>();
			a.Item = (Terminal<char>)'a';
			b = new Optional<char>();
			b.Item = (Terminal<char>)'a';

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Optional<char> a, b;


			a = new Optional<char>();
			a.Item = (Terminal<char>)'a';
			b = new Optional<char>();
			b.Item = (Terminal<char>)'b';

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal<char>()));
			Assert.IsFalse(a.Equals(new EOS<char>()));


		}



	}
}
