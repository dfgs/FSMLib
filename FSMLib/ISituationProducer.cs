using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public interface ISituationProducer<T>
	{
		IEnumerable<IInput<T>> GetDistinctInputs(IEnumerable<Situation<T>> Situations);
	}
}
