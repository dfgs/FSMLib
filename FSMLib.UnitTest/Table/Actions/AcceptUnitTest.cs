using System;
using FSMLib.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables.Actions
{
	[TestClass]
	public class AcceptUnitTest
	{
		

		[TestMethod]
		public void ShouldEquals()
		{
			Accept<char> a,b;

			a = new Accept<char>();
			b = new Accept<char>();
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}




	}
}
