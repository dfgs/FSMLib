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
	public class OneOrMore<T> : ExtendedPredicate<T>
	{

		public BasePredicate<T> Item
		{
			get;
			set;
		}

		public OneOrMore()
		{
		}

		
		

		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"◦{Item.ToString(CurrentPredicate)}+";
			else return $"{Item.ToString(CurrentPredicate)}+";
		}





	}
}
