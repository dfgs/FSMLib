using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.ProcessingQueues;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Tables
{
	public class LettersRangeInputCollection : IEnumerable<LettersRangeInput>
	{
		private List<LettersRangeInput> items;

		public int Count
		{
			get { return items.Count; }
		}

		public LettersRangeInputCollection()
		{
			items = new List<LettersRangeInput>();
		}


		
		public void Add(LetterInput Letter)
		{
			Add(new LettersRangeInput(Letter.Value, Letter.Value));
		}

		public void Add(LettersRangeInput Range)
		{
			ProcessingQueue<LettersRangeInput, bool> queue;
			LettersRangeInput existingRange;


			queue = new ProcessingQueue<LettersRangeInput, bool>();

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
							items.Insert(t, new LettersRangeInput(existingRange.FirstValue, (char)(range.FirstValue - 1))); t++;
						}
						items.Insert(t, range); t++;
						if (range.LastValue < existingRange.LastValue)
						{
							items.Insert(t, new LettersRangeInput((char)(range.LastValue + 1), existingRange.LastValue)); t++;
						}
						return;
					}

					// englobing
					if ((range.FirstValue <= existingRange.FirstValue) && (range.LastValue >= existingRange.LastValue))
					{
						if (range.FirstValue < existingRange.FirstValue)
						{
							items.Insert(t, new LettersRangeInput(range.FirstValue, (char)(existingRange.FirstValue - 1))); 
						}
						t++;
						if (range.LastValue > existingRange.LastValue)
						{
							q.Add(new LettersRangeInput((char)(existingRange.LastValue + 1), range.LastValue)); 
						}
						return;
					}

					// left
					if (range.FirstValue < existingRange.FirstValue)
					{
						items.RemoveAt(t);
						items.Insert(t, new LettersRangeInput(range.FirstValue, (char)(existingRange.FirstValue - 1))); t++;
						items.Insert(t, new LettersRangeInput(existingRange.FirstValue, range.LastValue)); t++;
						items.Insert(t, new LettersRangeInput((char)(range.LastValue+1), existingRange.LastValue)); t++;
						return;
					}

					// right
					if (range.FirstValue > existingRange.FirstValue)
					{
						items.RemoveAt(t);
						items.Insert(t, new LettersRangeInput(existingRange.FirstValue, (char)(range.FirstValue - 1))); t++;
						items.Insert(t, new LettersRangeInput(range.FirstValue, existingRange.LastValue)); t++;
						q.Add(new LettersRangeInput((char)(existingRange.LastValue + 1), range.LastValue)); t++;
						return;
					}
				}
				items.Add(range);

			});

		}


		public IEnumerator<LettersRangeInput> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
