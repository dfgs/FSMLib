using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class AnyLetterInput: IInput<char>
	{

		public AnyLetterInput()
		{

		}
		
		public bool Equals(IInput<char> other)
		{
			return (other is AnyLetterInput);
		}
		public bool Match(IInput<char> Other)
		{
			if (Other == null) return false;
			return (Other is ITerminalInput<char>) || (Other is AnyLetterInput);
		}

		public bool Match(char Value)
		{
			return true;
		}

		public override string ToString()
		{
			return ".";
		}
		public override int GetHashCode()
		{
			return ".".GetHashCode();
		}

	}
}
