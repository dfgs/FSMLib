using FSMLib.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationCollectionFactory<T>
	{
		IEnumerable<Situation<T>> CreateAxiomSituations();

		ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations);

		IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IActionInput<T> Input);


	}
}
