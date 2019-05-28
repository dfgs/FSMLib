using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Or<T>: ExtendedPredicate<T>
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

		
		


		public override string ToString(InputPredicate<T> CurrentPredicate)
		{
			if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
			return $"({string.Join("|", Items.Select(item => item.ToString(CurrentPredicate)))})";
			
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
				predicate.Items.Add((Terminal<T>)value);
			}
			return predicate;
		}
	}
}
