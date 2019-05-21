using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class AutomatonException<T>:Exception
	{
		public IInput<T> Input
		{
			get;
			private set;
		}

		public IEnumerable<BaseNode<T>> Stack
		{
			get;
			private set;
		}
		public AutomatonException(IInput<T> Input, IEnumerable<BaseNode<T>> Stack)
		{
			this.Input = Input;
			this.Stack = Stack;
		}

	}
}
