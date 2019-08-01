using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IDistinctInputFactory<T>
	{
		IEnumerable<IActionInput<T>> GetDistinctInputs(IEnumerable<IInput<T>> Inputs);
	}
}
