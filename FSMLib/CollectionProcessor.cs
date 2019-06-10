using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public static class CollectionProcessor
	{
		public static IEnumerable<T> DistinctEx<T>(this IEnumerable<T> Items)
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

		public static bool IsIndenticalToEx<T>(this IEnumerable<T> Items, IEnumerable<T> Other)
			where T : IEquatable<T>
		{
			if (Items == null) return Other==null;
			if (Other == null) return false;

			if (Items.Count() != Other.Count()) return false;

			foreach (T item in Items)
			{
				if (!Other.ContainsEx(item)) return false;
			}

			return true;
		}

		public static bool IsStrictelyIndenticalToEx<T>(this IEnumerable<T> Items, IEnumerable<T> Other)
			where T : IEquatable<T>
		{
			int count;

			if (Items == null) return Other == null;
			if (Other == null) return false;

			count = Items.Count();
			if (count != Other.Count()) return false;

			for(int t=0;t<count;t++)
			{
				if (!Items.ElementAt(t).Equals(Other.ElementAt(t))) return false;
			}

			return true;
		}
		public static bool ContainsEx<T>(this IEnumerable<T> Items, T Item)
			where T : IEquatable<T>
		{
			if (Items == null) return false;
			if (Item == null) return false;


			foreach (T item in Items)
			{
				if (item.Equals(Item)) return true;
			}

			return false;
		}

		public static IEnumerable<T> AsEnumerable<T>(this T Item)
		{
			yield return Item;
		}

	}
}
