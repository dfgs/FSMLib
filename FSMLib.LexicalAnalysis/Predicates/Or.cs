using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	[Serializable]
	public class Or:IOrPredicate<char>
	{

		[XmlArray]
		public List<IPredicate<char>> Items
		{
			get;
			set;
		}

		IEnumerable<IPredicate<char>> IOrPredicate<char>.Items => Items;

		public Or()
		{
			Items = new List<IPredicate<char>>();
		}



		public override string ToString()
		{
			return ToString(null);
		}


		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
			return $"({string.Join("|", Items.Select(item => item.ToString(CurrentPredicate)))})";

		}

		public bool Equals(IPredicate<char> other)
		{
			if (!(other is Or o)) return false;
			if (Items == null) return o.Items == null;
			return Items.IsStrictelyIndenticalToEx(o.Items);
		}

	}
}
