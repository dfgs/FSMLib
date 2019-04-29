using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Sequence<T> : RulePredicate<T>
	{

		[XmlArray]
		public List<RulePredicate<T>> Items
		{
			get;
			set;
		}

		public Sequence()
		{
			Items = new List<RulePredicate<T>>();
		}

		public override IEnumerable<RulePredicate<T>> Enumerate()
		{
			return Items.SelectMany(item => item.Enumerate());
		}

		/*public override string ToParenthesisString(RulePredicate<T> Current)
		{
			if (Items.Count == 1) return Items[0].ToParenthesisString(Current);
			return $"({string.Join("", Items.Select(item => item.ToParenthesisString(Current)))})";
		}
		public override string ToString(RulePredicate<T> Current)
		{
			return string.Join("", Items.Select(item => item.ToParenthesisString(Current)));
		}*/
		public override string ToParenthesisString()
		{
			if (Items.Count == 1) return Items[0].ToParenthesisString();
			return $"({string.Join("", Items.Select(item => item.ToParenthesisString()))})";
		}
		public override string ToString()
		{
			return string.Join("", Items.Select(item => item.ToParenthesisString()));
		}

	
		public static implicit operator Sequence<T>(RulePredicate<T>[] Values)
		{
			Sequence<T> predicate;

			predicate = new Sequence<T>();
			predicate.Items.AddRange(Values);

			return predicate;
		}
		public static implicit operator Sequence<T>(T[] Values)
		{
			Sequence<T> predicate;

			predicate = new Sequence<T>();
			foreach (T value in Values)
			{
				predicate.Items.Add((One<T>)value);
			}
			return predicate;
		}

	}
}
