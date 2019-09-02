using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxisAnalysis.Inputs
{
	public class AnyTerminalInput : IActionInput<Token>
	{
		public AnyTerminalInput() 
		{
		}

		public bool Equals(IInput<Token> other)
		{
			return other is AnyTerminalInput;
		}

		public bool Match(IInput<Token> Other)
		{
			return (Other is AnyTerminalInput) || (Other is TerminalInput);
		}

		public bool Match(Token Value)
		{
			return true;
		}
	}
}
