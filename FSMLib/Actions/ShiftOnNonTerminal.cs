
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public class ShiftOnNonTerminal<T>:Shift<T>,IEquatable<ShiftOnNonTerminal<T>>
	{

		
		public string Name
		{
			get;
			set;
		}

	

		/*public bool Match(string Other)
		{
			return Name == Other;
		}*/
		public override string ToString()
		{
			return Name;
		}

		public bool Equals(ShiftOnNonTerminal<T> other)
		{
			if (other == null) return false;
			return (other.Name == Name) && (other.TargetStateIndex == TargetStateIndex);
		}


	}
}
