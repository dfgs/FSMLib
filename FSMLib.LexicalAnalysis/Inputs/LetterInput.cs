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

		


		public bool Match(IInput<char> Other)
		{
			if (Other == null) return false;
			if (Other is LetterInput o)
			{
				return o.Value == this.Value;
			}
			return false;
		}

		public bool Match(char Value)
		{
			return Value==this.Value;
		}

		public override string ToString()
		{
			return Value.ToString() ;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

	}
}
