using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	public class ReducePredicate<T>:InputPredicate<T>
	{
		public ReducePredicate()
		{

		}



		public override IInput<T> GetInput()
		{
			return null;
		}

		
		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•";
			return "";
		}
	}
}
