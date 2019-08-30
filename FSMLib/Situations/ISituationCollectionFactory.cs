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
		IEnumerable<ISituation<T>> CreateAxiomSituations();

		ISituationCollection<T> Develop(IEnumerable<ISituation<T>> Situations);

		IEnumerable<ISituation<T>> CreateNextSituations(IEnumerable<ISituation<T>> CurrentSituations, IActionInput<T> Input);


	}
}
