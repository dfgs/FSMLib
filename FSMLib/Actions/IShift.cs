using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public  interface IShift<T>:IAction<T>, IEquatable<IShift<T>>
	{
		IActionInput<T> Input
		{
			get;
		}

		int TargetStateIndex
		{
			get;
		}

		

		

	}
}
