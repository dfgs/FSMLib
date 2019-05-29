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

		//string GetReduction(InputPredicate<T> CurrentPredicate);
		IEnumerable<InputPredicate<T>> GetRootInputPredicates(BasePredicate<T> RootPredicate);
		IEnumerable<InputPredicate<T>> GetNextPredicates(InputPredicate<T> CurrentPredicate);
		bool Contains(InputPredicate<T> Predicate);

		bool CanReduce(InputPredicate<T> CurrentPredicate);
	}
}
