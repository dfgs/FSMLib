using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class ZeroOrMore: IZeroOrMorePredicate<char>
	{

		public IPredicate<char> Item
		{
			get;
			set;
		}

		public ZeroOrMore()
		{
		}




		public override string ToString()
		{
			return ToString(null);
		}


		public  string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}*";
		}

		public  bool Equals(IPredicate<char> other)
		{
			if (!(other is ZeroOrMore o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}


	}
}
