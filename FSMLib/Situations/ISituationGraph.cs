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


		IEnumerable<Situation<T>> CreateAxiomSituations();

		ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations);

		IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IInput<T> Input);
		
		bool Contains(SituationPredicate<T> Predicate);
		
	}
}
