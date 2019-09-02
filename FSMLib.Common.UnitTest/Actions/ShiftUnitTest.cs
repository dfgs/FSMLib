using System;
using FSMLib.Actions;
using FSMLib.Common.Actions;
using FSMLib.Common.UnitTest.Mocks;
using FSMLib.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.Common.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class ShiftUnitTest
	{

		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Shift<char>(null, 1));
		}
		[TestMethod]
		public void ShouldEquals()
		{
			Shift<char> a, b,c,d;

			a = new Shift<char>(new MockedReduceInput(),1 );
			b = new Shift<char>(new MockedReduceInput(), 1);
			c = new Shift<char>(new MockedReduceInput(), 2);
			d = new Shift<char>(new MockedReduceInput(),2);
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
			Assert.IsTrue(c.Equals(d));
			Assert.IsTrue(d.Equals(c));
		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Shift<char> a, b,c,d;



			a = new Shift<char>(new MockedReduceInput(),1 );
			b = new Shift<char>(new MockedReduceInput2(), 1);
			c = new Shift<char>(new MockedReduceInput(), 2);
			d = new Shift<char>(new MockedReduceInput2(),2);


			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
			Assert.IsFalse(a.Equals(c));
			Assert.IsFalse(b.Equals(c));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(d));
			Assert.IsFalse(d.Equals(a));
		}

		
	}
}
