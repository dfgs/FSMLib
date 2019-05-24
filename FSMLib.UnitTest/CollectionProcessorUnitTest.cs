using System;
using System.Collections.Generic;
using System.Linq;
using FSMLib.Table;
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
			items = CollectionProcessor.DistinctEx(a).ToArray();
			Assert.AreEqual(3, items.Length);
			Assert.AreEqual(1, items[0]);
			Assert.AreEqual(2, items[1]);
			Assert.AreEqual(3, items[2]);
			a.Add(1);
			a.Add(2);
			a.Add(3);
			a.Add(1);
			items = CollectionProcessor.DistinctEx(a).ToArray();
			Assert.AreEqual(3, items.Length);
			Assert.AreEqual(1, items[0]);
			Assert.AreEqual(2, items[1]);
			Assert.AreEqual(3, items[2]);

		}
		[TestMethod]
		public void ShouldNotReturnDistinct()
		{
			int[] items;

			items = CollectionProcessor.DistinctEx<int>(null).ToArray();
			Assert.AreEqual(0, items.Length);
		}

		[TestMethod]
		public void ShouldReturnTrueIfContainsItems()
		{
			List<int> a;

			a = new List<int>();
			a.Add(1);
			a.Add(2);
			a.Add(3);

			Assert.IsTrue(CollectionProcessor.ContainsEx(a, 1));
			Assert.IsTrue(CollectionProcessor.ContainsEx(a, 2));
			Assert.IsTrue(CollectionProcessor.ContainsEx(a, 3));
		}

		[TestMethod]
		public void ShouldReturnFalseIfDoesntContainItems()
		{
			List<int> a;

			a = new List<int>();
			a.Add(1);
			a.Add(2);
			a.Add(3);

			Assert.IsFalse(CollectionProcessor.ContainsEx(a, 4));
			Assert.IsFalse(CollectionProcessor.ContainsEx(a, -1));
			Assert.IsFalse(CollectionProcessor.ContainsEx(null, 1));
		}


		[TestMethod]
		public void ShouldReturnTrueIfCollectionsAreIdentical()
		{
			List<int> a,b;

			a = new List<int>();
			a.Add(1);
			a.Add(2);
			a.Add(3);
			b = new List<int>();
			b.Add(3);
			b.Add(1);
			b.Add(2);


			Assert.IsTrue(CollectionProcessor.IsIndenticalToEx(a,b));

		}

		[TestMethod]
		public void ShouldReturnFalseIfCollectionsAreNotIdentical()
		{
			List<int> a, b,c;

			a = new List<int>();
			a.Add(1);
			a.Add(2);
			a.Add(3);
			b = new List<int>();
			b.Add(4);
			b.Add(1);
			b.Add(2);
			c = new List<int>();
			c.Add(1);
			c.Add(2);


			Assert.IsFalse(CollectionProcessor.IsIndenticalToEx(a, b));
			Assert.IsFalse(CollectionProcessor.IsIndenticalToEx(a, c));
			Assert.IsFalse(CollectionProcessor.IsIndenticalToEx(a, null));
			Assert.IsFalse(CollectionProcessor.IsIndenticalToEx(null, a));

		}

		[TestMethod]
		public void ShouldEnumerateSingleItem()
		{
			int[] a;

			a = 1.AsEnumerable().ToArray();

			Assert.AreEqual(1, a.Length);
			Assert.AreEqual(1, a[0]);


		}


	}
}
