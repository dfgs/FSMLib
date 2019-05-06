using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Or<T>:BasePredicate<T>
	{
	
		[XmlArray]
		public List<BasePredicate<T>> Items
		{
			get;
			set;
		}

		public Or()
		{
			Items = new List<BasePredicate<T>>();
		}

		public override IEnumerable<BasePredicate<T>> Enumerate()
		{
			return Items.SelectMany(item => item.Enumerate());
		}

		/*public override string ToParenthesisString(RulePredicate<T> Current)
		{
			if (Items.Count == 1) return Items[0].ToParenthesisString(Current);
			return $"({string.Join("|", Items.Select(item => item.ToParenthesisString(Current)))})";
		}
		public override string ToString(RulePredicate<T> Current)
		{
			return string.Join("|", Items.Select(item => item.ToParenthesisString(Current)));
		}*/
		public override string ToParenthesisString()
		{
			if (Items.Count == 1) return Items[0].ToParenthesisString();
			return $"({string.Join("|", Items.Select(item => item.ToParenthesisString()))})";
		}
		public override string ToString()
		{
			return string.Join("|", Items.Select(item => item.ToParenthesisString()));
		}
		public static implicit operator Or<T>(BasePredicate<T>[] Values)
		{
			Or<T> predicate;

			predicate = new Or<T>();
			predicate.Items.AddRange(Values);

			return predicate;
		}
		public static implicit operator Or<T>(T[] Values)
		{
			Or<T> predicate;

			predicate = new Or<T>();
			foreach (T value in Values)
			{
				predicate.Items.Add((One<T>)value);
			}
			return predicate;
		}
	}
}
