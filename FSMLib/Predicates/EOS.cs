using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public class EOS<T>: InputPredicate<T>
	{
		public override BaseInput<T> GetInput()
		{
			return new EOSInput<T>();
		}


		

		public override string ToString(InputPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•¤";
			else return "¤";
		}

		






	}
}
