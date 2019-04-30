using System;
using FSMLib.Graphs;
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
			Graph<char> graph;

			graph = new Graph<char>();
			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			Assert.AreEqual(true, a.Equals(b));
			Assert.AreEqual(true, b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			Situation<char> a, b;
			Graph<char> graph;

			graph = new Graph<char>();
			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 1 };
			Assert.AreEqual(false, a.Equals(b));
			Assert.AreEqual(false, b.Equals(a));
			Assert.AreEqual(false, a.Equals(null));
			Assert.AreEqual(false, b.Equals(null));
		}

	}
}
