using FSMLib.Rules;
using FSMLib.Situations;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IAutomatonTableTuple<T>
	{
		IState<T> State
		{
			get;
		}
		ISituationCollection<T> Situations
		{
			get;
		}

		
	}
}
