using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class OneInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			OneInput<char> input;

			input = new OneInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchOneInput()
		{
			OneInput<char> input;
			IInput<char> other;

			input = new OneInput<char>() { Value = 'a' };
			other = new OneInput<char>() { Value = 'a' };
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('a'));
		}

		[TestMethod]
		public void ShouldNotMatchOneInput()
		{
			OneInput<char> input;
			IInput<char> other;

			input = new OneInput<char>() { Value = 'a' };
			other = new OneInput<char>() { Value = 'b' };
			Assert.IsFalse(input.Match(other));
			Assert.IsFalse(input.Match('b'));
		}

		[TestMethod]
		public void ShouldNotMatchAnyInput()
		{
			OneInput<char> input;
			IInput<char> other;

			input = new OneInput<char>() { Value = 'a' };
			other = new AnyInput<char>();
			Assert.IsFalse(input.Match(other));
		}
		[TestMethod]
		public void ShouldBeEqual()
		{
			OneInput<char> a, b;

			a = new OneInput<char>() { Value = 'a' };
			b = new OneInput<char>() { Value = 'a' };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEqual()
		{
			OneInput<char> a, b;
			AnyInput<char> c;

			a = new OneInput<char>() { Value = 'a' };
			b = new OneInput<char>() { Value = 'b' };
			c = new AnyInput<char>();
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(c));
		}
		[TestMethod]
		public void ShouldConvertToString()
		{
			OneInput<char> a, b;

			a = new OneInput<char>() { Value = 'a' };
			b = new OneInput<char>() { Value = 'b' };
			Assert.AreEqual("a", a.ToString());
			Assert.AreEqual("b", b.ToString());
		}



	}
}
