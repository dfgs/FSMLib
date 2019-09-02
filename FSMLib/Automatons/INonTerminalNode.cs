using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public interface INonTerminalNode<T>:IBaseNode<T>
	{
		INonTerminalInput<T> Input
		{
			get;
		}

		IEnumerable<IBaseNode<T>> Nodes
		{
			get;
		}

		


	}
}
