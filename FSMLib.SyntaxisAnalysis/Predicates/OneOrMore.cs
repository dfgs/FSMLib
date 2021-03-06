﻿using FSMLib.Predicates;
using FSMLib.SyntaxicAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class OneOrMore: SyntaxicPredicate, IOneOrMorePredicate<Token>
	{
		[XmlIgnore]
		IPredicate<Token> IOneOrMorePredicate<Token>.Item => Item;


		public SyntaxicPredicate Item
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


		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}+";
		}


		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is OneOrMore o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}



	}
}
