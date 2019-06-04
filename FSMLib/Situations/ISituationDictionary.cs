using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationDictionary<T>
	{
		AutomatonTableTuple<T> GetTuple(ISituationCollection<T> Situations);
		AutomatonTableTuple<T> CreateTuple(State<T> State, ISituationCollection<T> Situations);


	}
}
