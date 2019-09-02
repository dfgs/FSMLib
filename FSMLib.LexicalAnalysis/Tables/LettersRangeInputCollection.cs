using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.ProcessingQueues;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Tables
{
	public class LettersRangeInputCollection : IEnumerable<TerminalsRangeInput>
	{
		private List<TerminalsRangeInput> items;

		public int Count
		{
			get { return items.Count; }
		}

		public LettersRangeInputCollection()
		{
			items = new List<TerminalsRangeInput>();
		}


		
		public void Add(TerminalInput Letter)
		{
			Add(new TerminalsRangeInput(Letter.Value, Letter.Value));
		}

		public void Add(TerminalsRangeInput Range)
		{
			ProcessingQueue<TerminalsRangeInput, bool> queue;
			TerminalsRangeInput existingRange;


			queue = new ProcessingQueue<TerminalsRangeInput, bool>();

			queue.Add(Range);
			queue.Process((q,range) =>
			{
				for (int t = 0; t < items.Count; t++)
				{
					existingRange = items[t];

					if ((range.LastValue < existingRange.FirstValue) || (range.FirstValue > existingRange.LastValue)) continue;

					// englobed
					if ((range.FirstValue >= existingRange.FirstValue) && (range.LastValue <= existingRange.LastValue))
					{
						items.RemoveAt(t);
						if (range.FirstValue > existingRange.FirstValue)
						{
							items.Insert(t, new TerminalsRangeInput(existingRange.FirstValue, (char)(range.FirstValue - 1))); t++;
						}
						items.Insert(t, range); t++;
						if (range.LastValue < existingRange.LastValue)
						{
							items.Insert(t, new TerminalsRangeInput((char)(range.LastValue + 1), existingRange.LastValue)); t++;
						}
						return;
					}

					// englobing
					if ((range.FirstValue <= existingRange.FirstValue) && (range.LastValue >= existingRange.LastValue))
					{
						if (range.FirstValue < existingRange.FirstValue)
						{
							items.Insert(t, new TerminalsRangeInput(range.FirstValue, (char)(existingRange.FirstValue - 1))); 
						}
						t++;
						if (range.LastValue > existingRange.LastValue)
						{
							q.Add(new TerminalsRangeInput((char)(existingRange.LastValue + 1), range.LastValue)); 
						}
						return;
					}

					// left
					if (range.FirstValue < existingRange.FirstValue)
					{
						items.RemoveAt(t);
						items.Insert(t, new TerminalsRangeInput(range.FirstValue, (char)(existingRange.FirstValue - 1))); t++;
						items.Insert(t, new TerminalsRangeInput(existingRange.FirstValue, range.LastValue)); t++;
						items.Insert(t, new TerminalsRangeInput((char)(range.LastValue+1), existingRange.LastValue)); t++;
						return;
					}

					// right
					if (range.FirstValue > existingRange.FirstValue)
					{
						items.RemoveAt(t);
						items.Insert(t, new TerminalsRangeInput(existingRange.FirstValue, (char)(range.FirstValue - 1))); t++;
						items.Insert(t, new TerminalsRangeInput(range.FirstValue, existingRange.LastValue)); t++;
						q.Add(new TerminalsRangeInput((char)(existingRange.LastValue + 1), range.LastValue)); t++;
						return;
					}
				}
				items.Add(range);

			});

		}


		public IEnumerator<TerminalsRangeInput> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
