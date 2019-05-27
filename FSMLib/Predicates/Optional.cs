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

		

		

		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"◦{Item.ToString(CurrentPredicate)}?";
			else return $"{Item.ToString(CurrentPredicate)}?";
		}





	}
}
