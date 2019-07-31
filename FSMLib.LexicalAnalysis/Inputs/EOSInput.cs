using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class EOSInput: ITerminalInput<char>, IActionInput<char>
	{

		
		
		public  bool Equals(IInput<char> other)
		{
			return other is EOSInput;
		}
		public  bool Match(IInput<char> Other)
		{
			if (Other == null) return false;
			return Other is EOSInput;
		}

		public  bool Match(char Value)
		{
			return false;
		}

		public override string ToString()
		{
			return "¤";
		}
		public override int GetHashCode()
		{
			return HashCodes.EOS ;
		}

	}
}
