using System;
using System.Collections.Generic;
using System.Linq;
using FSMLib.ProcessingQueues;
using FSMLib.Table;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.ProcessingQueues
{
	[TestClass]
	public class ProcessingQueueUnitTest
	{
		/*[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.Fail();
			//Assert.ThrowsException<ArgumentNullException>(() => new ProcessingQueue<string,string>(null));
		}*/

		[TestMethod]
		public void ShouldAddAndContains()
		{
			ProcessingQueue<string, string> queue;

			queue = new ProcessingQueue<string, string>( );
			for (int t = 0; t < 10; t++)
			{
				Assert.IsFalse(queue.Contains(t.ToString()));
				queue.Add(t.ToString());
				Assert.IsTrue(queue.Contains(t.ToString()));
			}
			

		}
		[TestMethod]
		public void ShouldProcess()
		{
			ProcessingQueue<string, string> queue;
			string[] results;

			queue = new ProcessingQueue<string, string>();
			for (int t = 0; t < 10; t++)
			{
				queue.Add(t.ToString());
			}
			queue.Process((q,item)=> 
			{
				q.Add(item);
				q.AddResult(item);
			});

			results = queue.Results.ToArray();
			Assert.AreEqual(10, results.Length);
			for (int t = 0; t < 10; t++)
			{
				Assert.AreEqual(t.ToString(), results[t]);
			}

		}
	}
}
