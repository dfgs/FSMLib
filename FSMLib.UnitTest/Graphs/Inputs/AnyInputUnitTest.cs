using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class AnyInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			AnyInput<char> input;

			input = new AnyInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchOneInput()
		{
			AnyInput<char> input;
			IInput<char> other;

			input = new AnyInput<char>();
			other = new OneInput<char>() { Value = 'a' };
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('a'));
		}



		[TestMethod]
		public void ShouldMatchAnyInput()
		{
			AnyInput<char> input;
			IInput<char> other;

			input = new AnyInput<char>();
			other = new AnyInput<char>();
			Assert.IsTrue(input.Match(other));
			Assert.IsTrue(input.Match('b'));
		}


	}
}
