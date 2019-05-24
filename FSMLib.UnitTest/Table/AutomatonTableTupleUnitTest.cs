using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.AutomatonTables
{
	[TestClass]
	public class AutomatonTableTupleUnitTest
	{
		[TestMethod]
		public void ShoudHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableTuple<object>(null, Enumerable.Empty<Situation<object>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableTuple<object>(new State<object>(), null));
		}
	}
}
