using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class SituationUnitTest
	{
		[TestMethod]
		public void ShouldBeEquals()
		{
			Situation<char> a, b;
			AutomatonTable<char> automatonTable;

			automatonTable = new AutomatonTable<char>();
			a = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			b = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			Assert.AreEqual(true, a.Equals(b));
			Assert.AreEqual(true, b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			Situation<char> a, b;
			AutomatonTable<char> automatonTable;

			automatonTable = new AutomatonTable<char>();
			a = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			b = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 1 };
			Assert.AreEqual(false, a.Equals(b));
			Assert.AreEqual(false, b.Equals(a));
			Assert.AreEqual(false, a.Equals(null));
			Assert.AreEqual(false, b.Equals(null));
		}

	}
}
