using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class EOSInput<T>: BaseTerminalInput<T>
	{


		public override bool Equals(BaseTerminalInput<T> other)
		{
			return other is EOSInput<T>;
		}
		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			return Other is EOSInput<T>;
		}

		public override bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return "←";
		}
	}
}
