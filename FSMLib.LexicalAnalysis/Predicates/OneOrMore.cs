﻿using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class OneOrMore: LexicalPredicate, IOneOrMorePredicate<char>
	{
		[XmlIgnore]
		IPredicate<char> IOneOrMorePredicate<char>.Item => Item;


		public LexicalPredicate Item
		{
			get;
			set;
		}

		public OneOrMore()
		{
		}


		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}+";
		}


		public override bool Equals(IPredicate<char> other)
		{
			if (!(other is OneOrMore o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}



	}
}
