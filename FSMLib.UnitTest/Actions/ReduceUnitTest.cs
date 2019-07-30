using System;
using FSMLib.Actions;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ReduceUnitTest
	{

		[TestMethod]
		public void ShouldEquals()
		{
			Reduce<char> a, b;

			a = new Reduce<char>() { Name = "A" };
			b = new Reduce<char>() { Name = "A" };
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEquals()
		{
			Reduce<char> a, b;

			a = new Reduce<char>() { Name = "A" };
			b = new Reduce<char>() { Name = "B" };
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}

		[TestMethod]
		public void ShouldGetHashCode()
		{
			Reduce<char> a;
			LetterInput input;

			input = new LetterInput( 'a' );
			a = new Reduce<char>() { Name = "A",Input=input };
			Assert.AreEqual(input.GetHashCode(),a.GetHashCode());

		}

	}
}
