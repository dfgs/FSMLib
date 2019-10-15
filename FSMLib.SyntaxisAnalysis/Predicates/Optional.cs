using FSMLib.Predicates;
using FSMLib.SyntaxicAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class Optional: SyntaxicPredicate, IOptionalPredicate<Token>
	{
		[XmlIgnore]
		IPredicate<Token> IOptionalPredicate<Token>.Item => Item;
		

		public SyntaxicPredicate Item
		{
			get;
			set;
		}

		public Optional()
		{
		}


		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}?";
		}


		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is Optional o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}

	}
}
