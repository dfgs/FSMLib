using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class NonTerminalInput<T>:BaseInput<T>
	{
		public string Name
		{
			get;
			set;
		}

		
		public override bool Equals(IInput<T> other)
		{
			if (other is NonTerminalInput<T> nonTerminal)
			{
				return nonTerminal.Name == Name;

			}
			return false;
		}
		

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			if (Other is NonTerminalInput<T> nonTerminalInput) return nonTerminalInput.Name==Name;
			
			return false;
		}

		

		public override bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		
	}
}
