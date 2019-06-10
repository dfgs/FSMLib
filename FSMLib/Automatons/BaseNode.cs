using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Tables
{
	public abstract class BaseNode<T>
	{
		

		public abstract IEnumerable<BaseTerminalInput<T>> EnumerateInputs();
	}
}
