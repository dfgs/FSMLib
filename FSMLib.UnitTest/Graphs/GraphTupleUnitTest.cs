using System;
using System.Linq;
using FSMLib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs
{
	[TestClass]
	public class GraphTupleUnitTest
	{
		[TestMethod]
		public void ShoudHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new GraphTuple<object>(null, Enumerable.Empty<Situation<object>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphTuple<object>(new Node<object>(), null));
		}
	}
}
