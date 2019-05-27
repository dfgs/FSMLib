using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraph<T>
	{

		IEnumerable<InputPredicate<T>> GetNextPredicates(InputPredicate<T> CurrentPredicate);
		bool Contains(InputPredicate<T> Predicate);

	}
}
