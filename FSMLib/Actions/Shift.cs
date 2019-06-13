using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public  class Shift<T>:BaseAction<T>, IEquatable<Shift<T>>
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

		public bool Equals(Shift<T> other)
		{
			if (other == null) return false;
			if (other.TargetStateIndex != TargetStateIndex) return false;
			if (other.Input == null) return Input == null;
			return other.Input.Equals(Input);
		}

		public override int GetHashCode()
		{
			return Input.GetHashCode();
		}

	}
}
