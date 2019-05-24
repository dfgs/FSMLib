using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class TerminalInput<T>:BaseTerminalInput<T>,IEquatable<TerminalInput<T>>//,IEquatable<BaseInput<T>>
	{
		public T Value
		{
			get;
			set;
		}



		public bool Equals(TerminalInput<T> other)
		{
			if (other == null) return false;
			if (other.Value == null) return this.Value == null;
			return other.Value.Equals(this.Value);
		}

		/*public bool Equals(BaseInput<T> other)
		{
			if (other is TerminalInput<T> terminalInput) return Equals(terminalInput);
			return false;
		}*/

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

		
	}
}
