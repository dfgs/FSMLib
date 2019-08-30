using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Inputs
{
	public abstract class TerminalInput<T>:ITerminalInput<T>, IActionInput<T>
	{
		public T Value
		{
			get;
			set;
		}

		public TerminalInput(T Value)
		{
			this.Value = Value;
		}

		public bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			if (other is ITerminalInput<T> o)
			{
				return o.Value.Equals(this.Value);
			}
			return false;
		}



		public bool Match(T Input)
		{
			return Input.Equals(Value);
		}
		public bool Match(IInput<T> Input)
		{
			if (!(Input is ITerminalInput<T> o)) return false;
			return (o.Value.Equals(Value));
		}


		public override string ToString()
		{
			return Value.ToString() ;
		}

		

	}
}
