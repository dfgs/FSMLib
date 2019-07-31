using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class EOSInput<T>: ITerminalInput<T>, IActionInput<T>
	{

		
		
		public  bool Equals(IInput<T> other)
		{
			return other is EOSInput<T>;
		}
		public  bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			return Other is EOSInput<T>;
		}

		public  bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return "¤";
		}
		public override int GetHashCode()
		{
			return HashCodes.EOS ;
		}

	}
}
