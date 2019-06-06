using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public  class Shift<T>:BaseAction<T>
	{
		public BaseInput<T> Input
		{
			get;
			set;
		}

		public int TargetStateIndex
		{
			get;
			set;
		}

	}
}
