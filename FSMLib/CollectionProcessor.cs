using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public static class CollectionProcessor
	{
		public static IEnumerable<T> DisctinctEx<T>(this IEnumerable<T> Items)
			where T:IEquatable<T>
		{
			List<T> existing;

			if (Items == null) yield break;

			existing = new List<T>();
			foreach (T item in Items)
			{
				if (existing.Contains(item)) continue;
				yield return item;
				existing.Add(item);
			}

		}

	}
}
