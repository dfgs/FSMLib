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
	public abstract class BasePredicate<T>:IPredicate<T>
	{ 
		
		public override string ToString()
		{
			return ToString(null);
		}



		public abstract string ToString(ISituationPredicate<T> CurrentPredicate);



	}
}
