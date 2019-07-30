using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class LettersRangeInput:ITerminalInput<char>, IActionInput<char>
	{
		//private static Comparer<char> comparer = Comparer<char>.Default;
		public char FirstValue
		{
			get;
			set;
		}
		public char LastValue
		{
			get;
			set;
		}

		public LettersRangeInput(char FirstValue,char LastValue)
		{
			this.FirstValue = FirstValue;this.LastValue = LastValue;
		}

		public  bool Equals(IInput<char> other)
		{
			if (other == null) return false;
			if (other is LettersRangeInput o)
			{
				return o.FirstValue.Equals(this.FirstValue) && o.LastValue.Equals(this.LastValue);
			}
			return false;
		}




		public  bool Match(char Input)
		{
			return (Input >= FirstValue) && (Input <= LastValue);
		}
		public  bool Match(IInput<char> Input)
		{
			if ((Input is LetterInput terminal)) return Match(terminal.Value);
			else if (Input is LettersRangeInput terminalRange) return (terminalRange.FirstValue.Equals(this.FirstValue) && terminalRange.LastValue.Equals(this.LastValue));
			else return false;

		}

		public override string ToString()
		{
			return $"[{FirstValue}-{LastValue}]";
		}

		public override int GetHashCode()
		{
			return FirstValue.GetHashCode()*31+LastValue.GetHashCode();
		}

	}
}
