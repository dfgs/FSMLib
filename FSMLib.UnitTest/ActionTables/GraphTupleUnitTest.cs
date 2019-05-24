using System;
using System.Linq;
using FSMLib.ActionTables;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.ActionTables
{
	[TestClass]
	public class ActionTableTupleUnitTest
	{
		[TestMethod]
		public void ShoudHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableTuple<object>(null, Enumerable.Empty<Situation<object>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableTuple<object>(new State<object>(), null));
		}
	}
}
