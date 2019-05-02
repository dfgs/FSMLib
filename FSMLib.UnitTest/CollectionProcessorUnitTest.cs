using System;
using System.Collections.Generic;
using System.Linq;
using FSMLib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class CollectionProcessorUnitTest
	{
		[TestMethod]
		public void ShouldReturnDistinct()
		{
			List<int> a;
			int[] items;

			a = new List<int>();
			a.Add(1);
			a.Add(2);
			a.Add(3);
			items = CollectionProcessor.DisctinctEx(a).ToArray();
			Assert.AreEqual(3, items.Length);
			Assert.AreEqual(1, items[0]);
			Assert.AreEqual(2, items[1]);
			Assert.AreEqual(3, items[2]);
			a.Add(1);
			a.Add(2);
			a.Add(3);
			a.Add(1);
			items = CollectionProcessor.DisctinctEx(a).ToArray();
			Assert.AreEqual(3, items.Length);
			Assert.AreEqual(1, items[0]);
			Assert.AreEqual(2, items[1]);
			Assert.AreEqual(3, items[2]);

		}



	}
}
