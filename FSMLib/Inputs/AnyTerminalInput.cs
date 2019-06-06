using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class AnyTerminalInput<T>:BaseTerminalInput<T>//,IEquatable<BaseInput<T>>
	{


		
		public override bool Equals(IInput<T> other)
		{
			return other is AnyTerminalInput<T>;
		}

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			if (Other is TerminalInput<T> terminalInput) return true;
			
			return Other is AnyTerminalInput<T>;
		}

		public override bool Match(T Value)
		{
			return true;
		}

		public override string ToString()
		{
			return ".";
		}

		
	}
}
