using FSMLib.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationCollection<T> :  ISituationCollection<T>
	{
		private List<Situation<T>> items;

		public Situation<T> this[int Index]
		{
			get => items[Index];
		}
		public int Count => items.Count;

		public SituationCollection()
		{
			items = new List<Situation<T>>();
		}

		public void Add(Situation<T> Situation)
		{
			if (!Contains(Situation)) items.Add(Situation);
		}
		public void AddRange(IEnumerable<Situation<T>> Situations)
		{
			foreach(Situation<T> situation in Situations)
			{
				Add(situation);
			}
		}

		public bool Equals(ISituationCollection<T> Other)
		{
			if (Other == null) return false;

			if (Count != Other.Count) return false;

			foreach (Situation<T> item in items)
			{
				if (!Other.Contains(item)) return false;
			}

			return true;
		}
		public bool Contains(Situation<T> Situation)
		{
			return (items.FirstOrDefault(item => item.Equals(Situation)) != null);
		}

		public IEnumerable<Situation<T>> GetReductionSituations()
		{
			return items.Where(item => item.CanReduce && (item.Input!=null)).DistinctEx();
		}
		public IEnumerable<IInput<T>> GetNextInputs()
		{
			return items.SelectMany(item => item.Predicate.GetInputs()).DistinctEx();
		}
		

		public IEnumerator<Situation<T>> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}


		public override string ToString()
		{
			return string.Join("   -   ", items);
		}

	}
}
