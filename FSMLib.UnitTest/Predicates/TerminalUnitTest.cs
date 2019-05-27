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
			TerminalInput<char> input;

			predicate = new Terminal<char>() { Value = 'a' };
			input = predicate.GetInput() as TerminalInput<char>;
			Assert.IsNotNull(input);
			Assert.AreEqual('a', input.Value);
		}

		

	}
}
