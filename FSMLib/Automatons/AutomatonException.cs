
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Tables
{
	public class AutomatonException<T>:Exception
	{
		public T Input
		{
			get;
			private set;
		}

		public IEnumerable<BaseNode<T>> Stack
		{
			get;
			private set;
		}
		public AutomatonException(T Input, IEnumerable<BaseNode<T>> Stack)
		{
			this.Input = Input;
			this.Stack = Stack;
		}

	}
}
