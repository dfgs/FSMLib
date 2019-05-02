using FSMLib.Graphs;
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
		//IEnumerable<Transition<T>> GetAllTransitions(IEnumerable<Situation<T>> Situations);
		IEnumerable<IInput<T>> GetNextInputs(IEnumerable<Situation<T>> Situations);
		IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, IInput<T> Input);
	}
}
