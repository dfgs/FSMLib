
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Transitions
{
	public class TerminalTransition<T>:ShiftTransition<T>
	{

		
		public T Value
		{
			get;
			set;
		}


		// TODO: Add unit test
		public bool Match(T Other)
		{
			if (Value == null) return Other == null;
			else return Value.Equals(Other);
		}

		public override string ToString()
		{
			return Value?.ToString();
		}
	}
}
