using System;
using FSMLib.Table.Actions;
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

	}
}
