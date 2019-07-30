using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class ZeroOrMore: LexicalPredicate,IZeroOrMorePredicate<char>
	{

		[XmlIgnore]
		IPredicate<char> IZeroOrMorePredicate<char>.Item => Item;


		public LexicalPredicate Item
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


		public override  string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}*";
		}

		public override bool Equals(IPredicate<char> other)
		{
			if (!(other is ZeroOrMore o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}


	}
}
