using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.ProcessingQueues
{
	public  class ProcessingQueue<T, TResult>
	{
		private List<T> queue;
		private List<TResult> results;
		public IEnumerable<TResult> Results
		{
			get { return results; }
		}

		public ProcessingQueue()
		{
			queue = new List<T>();
			results = new List<TResult>();
		}
		public void Process(Action<ProcessingQueue<T, TResult>, T> ProcessFunction)
		{
			T item;
			List<T> closedList;

			closedList = new List<T>();
			while (queue.Count > 0)
			{
				item = queue[0];
				queue.RemoveAt(0);
				if (closedList.FirstOrDefault(existing => existing.Equals(item)) != null) continue;
				closedList.Add(item);
				ProcessFunction(this, item);

			}
		}

		public bool Contains(T Item)
		{
			return (queue.FirstOrDefault(item => item.Equals(Item)) != null);
		}
		public void Add(T Item)
		{
			if (Contains(Item)) return;
			queue.Add(Item);
		}
		public void AddRange(IEnumerable<T> Items)
		{
			foreach (T item in Items) Add(item);
		}

		public void AddResult(TResult Item)
		{
			results.Add(Item);
		}

	}
}
