using FSMLib.Inputs;
using FSMLib.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
	public interface ISituationProducer<T>
	{
		IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation<T>> Situations);
		IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<T>> Situations);
		IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, BaseTerminalInput<T> Input);
		IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, string Name);
	}
}
