
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table.Actions
{
	public class ShiftOnTerminal<T>:Shift<T>,IEquatable<ShiftOnTerminal<T>>
	{

		
		public T Value
		{
			get;
			set;
		}


	
		public bool Match(T Other)
		{
			if (Value == null) return Other == null;
			else return Value.Equals(Other);
		}

		public override string ToString()
		{
			return Value?.ToString();
		}

		public bool Equals(ShiftOnTerminal<T> other)
		{
			if (other == null) return false;
			if (other.TargetStateIndex != TargetStateIndex) return false;
			if (other.Value == null) return Value == null;
			return other.Value.Equals(Value);
		}

	}
}
