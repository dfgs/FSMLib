using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class SituationPredicate<T>: BasePredicate<T>,ISituationPredicate<T>
	{

		public abstract bool Match(T Input);
		public abstract bool Match(IInput<T> Input);

	}
}
