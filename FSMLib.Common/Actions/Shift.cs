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
		}

		public int TargetStateIndex
		{
			get;
		}

		public Shift(IActionInput<T> Input,int TargetStateIndex)
		{
			if (Input == null) throw new ArgumentNullException("Input");

			this.Input = Input;
			this.TargetStateIndex = TargetStateIndex;
		}

		public bool Equals(IShift<T> other)
		{
			if (other == null) return false;
			return (TargetStateIndex==other.TargetStateIndex) && (Input.Equals(other.Input));
		}

		

	}
}
