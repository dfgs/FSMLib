using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class TerminalInput<T>:BaseTerminalInput<T>, IActionInput<T>
	{
		public T Value
		{
			get;
			set;
		}

		

		public override bool Equals(IInput<T> other)
		{
			if (other is TerminalInput<T> o)
			{
				if (o.Value == null) return this.Value == null;
				return o.Value.Equals(this.Value);

			}
			return false;
		}

		


		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			if (Other is TerminalInput<T> terminalInput) return Equals(terminalInput);
			
			return false;
		}

		public override bool Match(T Value)
		{
			if (Value == null) return this.Value == null;
			return Value.Equals(this.Value);
		}

		public override string ToString()
		{
			return Value.ToString() ;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

	}
}
