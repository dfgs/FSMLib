using FSMLib.Common.ProcessingQueues;
using FSMLib.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common
{
	public class TerminalRangeInputCollection<T> : IEnumerable<ITerminalRangeInput<T>>
		where T:IComparable<T>
	{
		private List<ITerminalRangeInput<T>> items;
		private IRangeValueProvider<T> provider;

		public int Count
		{
			get { return items.Count; }
		}

		public TerminalRangeInputCollection(IRangeValueProvider<T> RangeValueProvider)
		{
			if (RangeValueProvider == null) throw new ArgumentNullException("RangeValueProvider");
			this.provider = RangeValueProvider;
			items = new List<ITerminalRangeInput<T>>();
		}



		public void Add(T Value)
		{
			Add(provider.CreateTerminalRangeInput(Value, Value));
		}

		public void Add(ITerminalRangeInput<T> Range)
		{
			ProcessingQueue<ITerminalRangeInput<T>, bool> queue;
			ITerminalRangeInput<T> existingRange;


			queue = new ProcessingQueue<ITerminalRangeInput<T>, bool>();

			queue.Add(Range);
			queue.Process((q,range) =>
			{
				for (int t = 0; t < items.Count; t++)
				{
					existingRange = items[t];

					if ((range.LastValue.CompareTo(existingRange.FirstValue)<0) || (range.FirstValue.CompareTo(existingRange.LastValue)>0)) continue;

					// englobed
					if ((range.FirstValue.CompareTo(existingRange.FirstValue)>=0) && (range.LastValue.CompareTo(existingRange.LastValue)<=0))
					{
						items.RemoveAt(t);
						if (range.FirstValue.CompareTo(existingRange.FirstValue)>0)
						{
							items.Insert(t, provider.CreateTerminalRangeInput(existingRange.FirstValue, provider.GetPreviousValue(range.FirstValue ))); t++;
						}
						items.Insert(t, range); t++;
						if (range.LastValue.CompareTo(existingRange.LastValue)<0)
						{
							items.Insert(t, provider.CreateTerminalRangeInput(provider.GetNextValue(range.LastValue ), existingRange.LastValue)); t++;
						}
						return;
					}

					// englobing
					if ((range.FirstValue.CompareTo(existingRange.FirstValue)<=0) && (range.LastValue.CompareTo(existingRange.LastValue)>=0))
					{
						if (range.FirstValue.CompareTo(existingRange.FirstValue)<0)
						{
							items.Insert(t, provider.CreateTerminalRangeInput(range.FirstValue,provider.GetPreviousValue(existingRange.FirstValue ))); 
						}
						t++;
						if (range.LastValue.CompareTo(existingRange.LastValue)>0)
						{
							q.Add(provider.CreateTerminalRangeInput(provider.GetNextValue(existingRange.LastValue), range.LastValue)); 
						}
						return;
					}

					// left
					if (range.FirstValue.CompareTo(existingRange.FirstValue)<0)
					{
						items.RemoveAt(t);
						items.Insert(t, provider.CreateTerminalRangeInput(range.FirstValue, provider.GetPreviousValue(existingRange.FirstValue))); t++;
						items.Insert(t, provider.CreateTerminalRangeInput(existingRange.FirstValue, range.LastValue)); t++;
						items.Insert(t, provider.CreateTerminalRangeInput(provider.GetNextValue(range.LastValue), existingRange.LastValue)); t++;
						return;
					}

					// right
					if (range.FirstValue.CompareTo(existingRange.FirstValue)>0)
					{
						items.RemoveAt(t);
						items.Insert(t, provider.CreateTerminalRangeInput(existingRange.FirstValue, provider.GetPreviousValue(range.FirstValue ))); t++;
						items.Insert(t, provider.CreateTerminalRangeInput(range.FirstValue, existingRange.LastValue)); t++;
						q.Add(provider.CreateTerminalRangeInput(provider.GetNextValue(existingRange.LastValue), range.LastValue)); t++;
						return;
					}
				}
				items.Add(range);

			});

		}


		public IEnumerator<ITerminalRangeInput<T>> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
