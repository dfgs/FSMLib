
using FSMLib.Automatons;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Automatons
{
	public class AutomatonException<T>:Exception
	{
		public ITerminalInput<T> Input
		{
			get;
			private set;
		}

		public IEnumerable<IBaseNode<T>> Stack
		{
			get;
			private set;
		}
		public AutomatonException(ITerminalInput<T> Input, IEnumerable<IBaseNode<T>> Stack)
		{
			this.Input = Input;
			this.Stack = Stack;
		}

	}
}
