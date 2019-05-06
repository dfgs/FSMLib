using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class RuleInputUnitTest
	{
		[TestMethod]
		public void ShouldNotMatchNull()
		{
			RuleInput<char> input;

			input = new RuleInput<char>();
			Assert.IsFalse(input.Match(null));
		}

		[TestMethod]
		public void ShouldMatchRuleInput()
		{
			RuleInput<char> input;
			IInput<char> other;

			input = new RuleInput<char>() { Name = 'A' };
			other = new RuleInput<char>() { Name = 'A' };
			Assert.IsTrue(input.Match(other));
		}

		[TestMethod]
		public void ShouldNotMatchRuleInput()
		{
			RuleInput<char> input;
			IInput<char> other;

			input = new RuleInput<char>() { Name = 'A' };
			other = new RuleInput<char>() { Name = 'B' };
			Assert.IsFalse(input.Match(other));
			Assert.IsFalse(input.Match('A'));
		}

		[TestMethod]
		public void ShouldNotMatchAnyInput()
		{
			RuleInput<char> input;
			IInput<char> other;

			input = new RuleInput<char>() { Name = 'A' };
			other = new AnyInput<char>();
			Assert.IsFalse(input.Match(other));
		}
		[TestMethod]
		public void ShouldBeEqual()
		{
			RuleInput<char> a, b;

			a = new RuleInput<char>() { Name = 'A' };
			b = new RuleInput<char>() { Name = 'A' };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEqual()
		{
			RuleInput<char> a, b;
			AnyInput<char> c;

			a = new RuleInput<char>() { Name = 'A' };
			b = new RuleInput<char>() { Name = 'B' };
			c = new AnyInput<char>();
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(c));
		}
		[TestMethod]
		public void ShouldConvertToString()
		{
			RuleInput<char> a, b;

			a = new RuleInput<char>() { Name = 'A' };
			b = new RuleInput<char>() { Name = 'B' };
			Assert.AreEqual("A", a.ToString());
			Assert.AreEqual("B", b.ToString());
		}



	}
}
