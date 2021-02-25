using FSMLib.Attributes;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public interface IBaseNode<T>
	{

		IEnumerable<ITerminalInput<T>> EnumerateInputs();
	}
}
