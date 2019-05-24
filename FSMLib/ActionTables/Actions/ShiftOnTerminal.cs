
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables.Actions
{
	public class ShiftOnTerminal<T>:Shift<T>
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
