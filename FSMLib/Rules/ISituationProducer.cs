using FSMLib.ActionTables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
	public interface ISituationProducer<T>
	{
		IEnumerable<T> GetNextTerminals(IEnumerable<Situation<T>> Situations);
		IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<T>> Situations);
		IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, T Value);
		IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, string Name);
	}
}
