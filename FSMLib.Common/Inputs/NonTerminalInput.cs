using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Inputs
{
	public abstract class NonTerminalInput<T>:INonTerminalInput<T>
	{
		public string Name
		{
			get;
			set;
		}

		
		public NonTerminalInput(string Name)
		{
			this.Name = Name;
		}
		
		public  bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			if (other is INonTerminalInput<T> nonTerminal)
			{
				return nonTerminal.Name == Name;

			}
			return false;
		}


		public bool Match(T Input)
		{
			return false;
		}
		public bool Match(IInput<T> Input)
		{
			if (!(Input is INonTerminalInput<T> o)) return false;
			if (Name == null) return o.Name == null;
			return Name == o.Name;
		}

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		
	}
}
