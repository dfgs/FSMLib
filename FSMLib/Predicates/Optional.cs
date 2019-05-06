using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Optional<T> : BasePredicate<T>
	{

		public BasePredicate<T> Item
		{
			get;
			set;
		}

		public Optional()
		{
		}

		public override IEnumerable<BasePredicate<T>> Enumerate()
		{
			if (Item == null) yield break;
			foreach (BasePredicate<T> item in Item.Enumerate()) yield return item;
		}

		
		public override string ToParenthesisString()
		{
			return $"{Item.ToParenthesisString()}?";
		}

		public override string ToString()
		{
			return $"{Item.ToParenthesisString()}?";
		}


		

	}
}
