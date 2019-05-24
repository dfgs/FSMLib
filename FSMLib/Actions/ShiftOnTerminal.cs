
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public class ShiftOnTerminal<T>:Shift<T>,IEquatable<ShiftOnTerminal<T>>
	{

		
		public TerminalInput<T> Input
		{
			get;
			set;
		}


	
		/*public bool Match(T Other)
		{
			if (Input == null) return Other == null;
			else return Input.Equals(Other);
		}*/

		public override string ToString()
		{
			return Input?.ToString();
		}

		public bool Equals(ShiftOnTerminal<T> other)
		{
			if (other == null) return false;
			if (other.TargetStateIndex != TargetStateIndex) return false;
			if (other.Input == null) return Input == null;
			return other.Input.Equals(Input);
		}

	}
}
