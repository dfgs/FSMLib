using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class LetterInput:ITerminalInput<char>, IActionInput<char>
	{
		public char Value
		{
			get;
			set;
		}

		public LetterInput(char Value)
		{
			this.Value = Value;
		}

		public bool Equals(IInput<char> other)
		{
			if (other == null) return false;
			if (other is LetterInput o)
			{
				return o.Value==this.Value;
			}
			return false;
		}



		public bool Match(char Input)
		{
			return Input == Value;
		}
		public bool Match(IInput<char> Input)
		{
			if (!(Input is LetterInput o)) return false;
			return (o.Value == Value);
		}


		public override string ToString()
		{
			return Value.ToString() ;
		}

		

	}
}
