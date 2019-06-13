using FSMLib.Inputs;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class ZeroOrMorePredicate<T> : ExtendedPredicate<T>
	{

		public BasePredicate<T> Item
		{
			get;
			set;
		}

		public ZeroOrMorePredicate()
		{
		}

		

	


		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}*";
		}

		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is ZeroOrMorePredicate<T> o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}



	}
}
