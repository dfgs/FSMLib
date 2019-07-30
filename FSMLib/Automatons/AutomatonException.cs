
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class AutomatonException<T>:Exception
	{
		public ITerminalInput<T> Input
		{
			get;
			private set;
		}

		public IEnumerable<BaseNode<T>> Stack
		{
			get;
			private set;
		}
		public AutomatonException(ITerminalInput<T> Input, IEnumerable<BaseNode<T>> Stack)
		{
			this.Input = Input;
			this.Stack = Stack;
		}

	}
}
