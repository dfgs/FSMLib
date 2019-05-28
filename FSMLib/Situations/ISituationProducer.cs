using FSMLib.Actions;
using FSMLib.Inputs;
using FSMLib.Rules;
using FSMLib.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationProducer<T>
	{
		IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation2<T>> Situations);
		IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation2<T>> Situations);
		IEnumerable<Situation2<T>> GetNextSituations(ISituationGraph<T> SituationGraph, IEnumerable<Situation2<T>> Situations, IInput<T> Input);
		void Connect(IEnumerable<State<T>> States, IEnumerable<BaseAction<T>> Actions);
		IEnumerable<Situation2<T>> Develop(ISituationGraph<T> SituationGraph,IEnumerable<Situation2<T>> Situations,IEnumerable<Rule<T>> Rules);
	}
}
