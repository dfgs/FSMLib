using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Common.Table;
using FSMLib.Common.Situations;

namespace FSMLib.Common.UnitTest.AutomatonTables
{
	[TestClass]
	public class AutomatonTableTupleUnitTest
	{
		[TestMethod]
		public void ShoudHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>((Func<object>)(() => new AutomatonTableTuple<object>(null, new SituationCollection<object>() )));
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableTuple<object>(new State<object>(), null));
		}
	}
}
