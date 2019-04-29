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
		}

		[TestMethod]
		public void ShouldNotMatchOneInput()
		{
			OneInput<char> input;
			IInput<char> other;

			input = new OneInput<char>() { Value = 'a' };
			other = new OneInput<char>() { Value = 'b' };
			Assert.IsFalse(input.Match(other));
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


	}
}
