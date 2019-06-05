using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraph<T>
	{
		//IEnumerable<BaseInput<T>> GetInputsAfterPredicate(BasePredicate<T> CurrentPredicate);

		ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations);

		IEnumerable<Situation<T>> GetNextSituations(Situation<T> CurrentSituation);
		bool Contains(BasePredicate<T> Predicate);

	}
}
