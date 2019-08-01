using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IAutomatonTableFactory<T>
	{
		AutomatonTable<T> BuildAutomatonTable(ISituationCollectionFactory<T> SituationCollectionFactory,IDistinctInputFactory<T> DistinctInputFactory);

	}
}
