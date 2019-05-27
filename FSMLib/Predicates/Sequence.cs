using FSMLib.Inputs;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Sequence<T> : ExtendedPredicate<T>
	{

		[XmlArray]
		public List<BasePredicate<T>> Items
		{
			get;
			set;
		}

		public Sequence()
		{
			Items = new List<BasePredicate<T>>();
		}

		

		

		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this)
			{
				if (Items.Count == 1) return $"◦{Items[0].ToString(CurrentPredicate)}";
				return $"◦({string.Join("", Items.Select(item => item.ToString(CurrentPredicate)))})";
			}
			else
			{
				if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
				return $"({string.Join("", Items.Select(item => item.ToString(CurrentPredicate)))})";
			}
		}

	
		public static implicit operator Sequence<T>(BasePredicate<T>[] Values)
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
				predicate.Items.Add((Terminal<T>)value);
			}
			return predicate;
		}

	}
}
