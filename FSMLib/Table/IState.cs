using FSMLib.Actions;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IState<T>
	{
		IEnumerable<IShift<T>> ShiftActions
		{
			get;
		}

		IEnumerable<IReduce<T>> ReduceActions
		{
			get;
		}

		int ShiftActionCount
		{
			get;
		}

		int ReduceActionCount
		{
			get;
		}

		void Add(IShift<T> Action);
		void Add(IReduce<T> Action);
		IShift<T> GetShift(IActionInput<T> Input);
		IReduce<T> GetReduce(IActionInput<T> Input);
		



	}
}
