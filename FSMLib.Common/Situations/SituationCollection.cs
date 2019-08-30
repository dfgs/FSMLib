using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Situations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Situations
{
	public class SituationCollection<T> :  ISituationCollection<T>
	{
		private readonly List<ISituation<T>> items;

		public ISituation<T> this[int Index]
		{
			get => items[Index];
		}
		public int Count => items.Count;

		public SituationCollection()
		{
			items = new List<ISituation<T>>();
		}

		public void Add(ISituation<T> Situation)
		{
			if (!Contains(Situation)) items.Add(Situation);
		}
		public void AddRange(IEnumerable<ISituation<T>> Situations)
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
		public bool Contains(ISituation<T> Situation)
		{
			return (items.FirstOrDefault(item => item.Equals(Situation)) != null);
		}

		public IEnumerable<ISituation<T>> GetReductionSituations()
		{
			return items.Where(item => item.CanReduce ).DistinctEx();
		}

		


		public IEnumerator<ISituation<T>> GetEnumerator()
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
