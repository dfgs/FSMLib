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
	public class ZeroOrMore<T> : ExtendedPredicate<T>
	{

		public BasePredicate<T> Item
		{
			get;
			set;
		}

		public ZeroOrMore()
		{
		}

		

	


		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			return $"{Item.ToString(CurrentPredicate)}*";
		}


		

	}
}
