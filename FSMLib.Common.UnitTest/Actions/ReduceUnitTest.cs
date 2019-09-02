using System;
using FSMLib.Actions;
using FSMLib.Common.Actions;
using FSMLib.Common.UnitTest.Mocks;
using FSMLib.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.Common.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ReduceUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Reduce<char>(null, true, new MockedReduceInput()));
			Assert.ThrowsException<ArgumentNullException>(() => new Reduce<char>("Name", true, null));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			Reduce<char> a, b;

			a = new Reduce<char>( "A" ,true,new MockedReduceInput());
			b = new Reduce<char>("A", false, new MockedReduceInput2());
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEquals()
		{
			Reduce<char> a, b;

			a = new Reduce<char>("A", true, new MockedReduceInput());
			b = new Reduce<char>("B", true, new MockedReduceInput());
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}

		

	}
}
