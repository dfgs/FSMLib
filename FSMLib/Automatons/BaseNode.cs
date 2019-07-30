using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public abstract class BaseNode<T>
	{
		

		public abstract IEnumerable<ITerminalInput<T>> EnumerateInputs();
	}
}
