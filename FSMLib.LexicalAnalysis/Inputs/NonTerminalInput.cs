using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class NonTerminalInput:INonTerminalInput<char>
	{
		public string Name
		{
			get;
			set;
		}

		public NonTerminalInput()
		{

		}
		public NonTerminalInput(string Name)
		{
			this.Name = Name;
		}
		
		public  bool Equals(IInput<char> other)
		{
			if (other == null) return false;
			if (other is NonTerminalInput nonTerminal)
			{
				return nonTerminal.Name == Name;

			}
			return false;
		}


		public bool Match(char Input)
		{
			return false;
		}
		public bool Match(IInput<char> Input)
		{
			if (!(Input is NonTerminalInput o)) return false;
			if (Name == null) return o.Name == null;
			return Name == o.Name;
		}

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		
	}
}
