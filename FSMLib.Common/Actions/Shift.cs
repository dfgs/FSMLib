using FSMLib.Actions;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Actions
{
	public  class Shift<T>:IShift<T>
	{
		public IActionInput<T> Input
		{
			get;
			set;
		}

		public int TargetStateIndex
		{
			get;
			set;
		}

		public bool Equals(IShift<T> other)
		{
			if (other == null) return false;
			if (other.TargetStateIndex != TargetStateIndex) return false;
			if (other.Input == null) return Input == null;
			return other.Input.Equals(Input);
		}

		

	}
}
