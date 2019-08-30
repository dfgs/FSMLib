using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class TerminalInput:TerminalInput<char>
	{
		

		public TerminalInput(char Value):base(Value)
		{
			this.Value = Value;
		}


		

	}
}
