using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis
{
	public class StringIncrementer
	{
		public string Inc(string Value)
		{
			char lastChar;

			if ((Value == null) || (Value==Token.MaxStringValue)) throw new ArgumentOutOfRangeException("Value");

			if (Value.Length == 0) return "\0";

			lastChar = Value[Value.Length - 1];
			if (lastChar==char.MaxValue)
			{
				return Value + char.MinValue;
			}
			return Value.Substring(0, Value.Length - 1) + (char)(lastChar + 1);
		}

		public string Dec(string Value)
		{
			char lastChar;

			if ((Value==null) || (Value.Length == 0)) throw new ArgumentOutOfRangeException("Value");

			lastChar = Value[Value.Length - 1];
			if (lastChar == char.MinValue)
			{
				return Value.Substring(0, Value.Length - 1);
			}
			return Value.Substring(0, Value.Length - 1) + (char)(lastChar - 1);
		}


	}
}
