using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationDictionary<T>:IEnumerable<IAutomatonTableTuple<T>>
	{
		IAutomatonTableTuple<T> GetTuple(ISituationCollection<T> Situations);
		IAutomatonTableTuple<T> CreateTuple(IState<T> State, ISituationCollection<T> Situations);


	}
}
