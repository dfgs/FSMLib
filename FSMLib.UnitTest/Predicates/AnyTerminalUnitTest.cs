using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
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
		public void ShouldConvertToStringWithBullet()
		{
			AnyTerminal<char> predicate;

			predicate = new AnyTerminal<char>();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			AnyTerminal<char> predicate;


			predicate = new AnyTerminal<char>();

			Assert.IsTrue(predicate.Match(new TerminalInput<char>() { Value = 'a' }));
			Assert.IsTrue(predicate.Match(new EOSInput<char>()));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			AnyTerminal<char> predicate;


			predicate = new AnyTerminal<char>();

			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "B" }));

		}

		



		[TestMethod]
		public void ShouldEquals()
		{
			AnyTerminal<char> a, b;


			a = new AnyTerminal<char>();
			b = new AnyTerminal<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			AnyTerminal<char> a;


			a = new AnyTerminal<char>();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal<char>()));
			Assert.IsFalse(a.Equals(new EOS<char>()));


		}



	}
}
