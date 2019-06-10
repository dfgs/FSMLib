using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class TerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Terminal<char> predicate;

			predicate = new Terminal<char>() { Value = 'a' };

			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Terminal<char> predicate;

			predicate = new Terminal<char>() { Value = 'a' };

			Assert.AreEqual("•a", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldConvertImplicitelyFromValueType()
		{
			Terminal<char> predicate;

			predicate = 'a';
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);

		}
		[TestMethod]
		public void ShouldGetInput()
		{
			Terminal<char> predicate;
			IInput<char>[] inputs;

			predicate = new Terminal<char>() { Value = 'a' };
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('a', ((TerminalInput<char>)inputs[0]).Value);
		}
		[TestMethod]
		public void ShouldMatch()
		{
			Terminal<char> predicate;
			

			predicate = new Terminal<char>() { Value = 'a' };

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsFalse(predicate.Match('b'));

		}

		[TestMethod]
		public void ShouldEquals()
		{
			Terminal<char> a,b;


			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'a' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Terminal<char> a, b;


			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal<char>()));
			Assert.IsFalse(b.Equals(new EOS<char>()));


		}


	}
}
