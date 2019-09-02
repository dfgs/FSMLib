
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public interface ITerminalNode<T>:IBaseNode<T>
	{

		ITerminalInput<T> Input
		{
			get;
		}

	

	}
}
