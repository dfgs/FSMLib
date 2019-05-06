using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class AnyTerminalInput<T> : BaseInput<T>
	{
		public override int Priority => 1;

		public override bool Match(IInput<T> Other)
		{
			return (Other != null) && (!(Other is NonTerminalInput<T>)) ;
		}

		public override bool Match(T Other)
		{
			return Other != null;
		}

		public override bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			return (other is AnyTerminalInput<T>);
		}

		public override string ToString()
		{
			return ".";
		}

	}
}
