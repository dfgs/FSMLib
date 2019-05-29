using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class InputPredicate<T>: BasePredicate<T>
	{
		public abstract BaseInput<T> GetInput();

	}
}
