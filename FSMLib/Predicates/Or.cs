using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Or<T>:RulePredicate<T>
	{
	
		[XmlArray]
		public List<RulePredicate<T>> Items
		{
			get;
			set;
		}

		public Or()
		{
			Items = new List<RulePredicate<T>>();
		}

		public override IEnumerable<RulePredicate<T>> Enumerate()
		{
			return Items.SelectMany(item => item.Enumerate());
		}

		public override string ToParenthesisString(RulePredicate<T> Current)
		{
			if (Items.Count == 1) return Items[0].ToParenthesisString(Current);
			return $"({string.Join("|", Items.Select(item => item.ToParenthesisString(Current)))})";
		}
		public override string ToString(RulePredicate<T> Current)
		{
			return string.Join("|", Items.Select(item => item.ToParenthesisString(Current)));
		}
		public override string ToString()
		{
			return string.Join("|", Items.Select(item => item.ToParenthesisString(null)));
		}


	}
}
