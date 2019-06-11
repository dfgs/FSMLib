using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class TerminalRangeUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			TerminalRange<char> predicate;

			predicate = new TerminalRange<char>() { FirstValue = 'a',LastValue='c' };

			Assert.AreEqual("[a-c]", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			TerminalRange<char> predicate;

			predicate = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };

			Assert.AreEqual("•[a-c]", predicate.ToString(predicate));
		}

		

	
		[TestMethod]
		public void ShouldMatch()
		{
			TerminalRange<char> predicate;


			predicate = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsTrue(predicate.Match('a'));
			Assert.IsTrue(predicate.Match('b'));
			Assert.IsTrue(predicate.Match('c'));
			Assert.IsTrue(predicate.Match(new TerminalInput<char>() {Value='a' }));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			TerminalRange<char> predicate;


			predicate = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsFalse(predicate.Match('d'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'd' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));
			Assert.IsFalse(predicate.Match(new EOSInput<char>() ));

		}
		[TestMethod]
		public void ShouldEquals()
		{
			TerminalRange<char> a,b;


			a = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };
			b = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			TerminalRange<char> a, b;


			a = new TerminalRange<char>() { FirstValue = 'a', LastValue = 'c' };
			b = new TerminalRange<char>() { FirstValue = 'b', LastValue = 'c' };

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(null));
			Assert.IsFalse(b.Equals(new AnyTerminal<char>()));
			Assert.IsFalse(b.Equals(new EOS<char>()));


		}


	}
}
