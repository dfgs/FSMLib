using FSMLib.Automatons;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Automatons
{
	public abstract class BaseNode<T>:IBaseNode<T>
	{
		

		public abstract IEnumerable<ITerminalInput<T>> EnumerateInputs();
	}
}
