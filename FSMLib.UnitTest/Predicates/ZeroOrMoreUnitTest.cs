using FSMLib.Helpers;
using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class ZeroOrMoreUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			ZeroOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> terminal;

			terminal= new Terminal<char>() { Value = 'a' };
			predicate = new ZeroOrMore<char>();
			predicate.Item = terminal;

			Assert.AreEqual("a*", predicate.ToString());

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate = new ZeroOrMore<char>();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)*", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			ZeroOrMore<char> predicate;
			Sequence<char> sequence;
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new ZeroOrMore<char>();
			predicate.Item = terminal;

			Assert.AreEqual("•a*", predicate.ToString(terminal));
			Assert.AreEqual("◦a*", predicate.ToString(predicate));

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate = new ZeroOrMore<char>();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)*", predicate.ToString(terminal));
			Assert.AreEqual("◦(aaa)*", predicate.ToString(predicate));
		}

	

	}
}
