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
		

		public  bool Match(IInput<char> Other)
		{
			if (Other == null) return false;
			if (Other is NonTerminalInput nonTerminal)
			{
				return nonTerminal.Name == Name;

			}
			return false;
		}



		public  bool Match(char Value)
		{
			return false;
		}

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}
