using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class OneOrMore<T> : RulePredicate<T>
	{

		public RulePredicate<T> Item
		{
			get;
			set;
		}

		public OneOrMore()
		{
		}

		public override IEnumerable<RulePredicate<T>> Enumerate()
		{
			if (Item == null) yield break;
			foreach (RulePredicate<T> item in Item.Enumerate()) yield return item;
		}

		
		public override string ToParenthesisString()
		{
			return $"{Item.ToParenthesisString()}+";
		}

		public override string ToString()
		{
			return $"{Item.ToParenthesisString()}+";
		}


		

	}
}
