using FSMLib;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis
{
	public class RangeValueProvider : IRangeValueProvider<char>
	{
		public ITerminalRangeInput<char> CreateTerminalRangeInput(char FirstValue, char LastValue)
		{
			return new TerminalRangeInput(FirstValue, LastValue);
		}

		public char GetNextValue(char Value)
		{
			if (IsMaxValue(Value)) throw new ArgumentOutOfRangeException("Value");
			return (char)(Value + 1);
		}

		public char GetPreviousValue(char Value)
		{
			if (IsMinValue(Value)) throw new ArgumentOutOfRangeException("Value");
			return (char)(Value - 1);
		}

		public bool IsMaxValue(char Value)
		{
			return Value == char.MaxValue;
		}

		public bool IsMinValue(char Value)
		{
			return Value == char.MinValue;
		}
	}
}
