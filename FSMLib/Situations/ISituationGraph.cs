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

		IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IInput<T> Input);
		
		bool Contains(BasePredicate<T> Predicate);

	}
}
