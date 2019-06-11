using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public interface ISituationInputPredicate<T>:ISituationPredicate<T>
	{
		IEnumerable<IInput<T>> GetInputs();

	}
}
