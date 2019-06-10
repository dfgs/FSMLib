using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Optional<T> : ExtendedPredicate<T>
	{

		public BasePredicate<T> Item
		{
			get;
			set;
		}

		public Optional()
		{
		}

		

		

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}?";
		}


		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is Optional<T> o)) return false;
			if (Item == null) return o.Item == null;
			return Item.Equals(o.Item);
		}



	}
}
