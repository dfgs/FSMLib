using FSMLib;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis
{
	public class RangeValueProvider : IRangeValueProvider<Token>
	{
		private static StringIncrementer incrementer;

		public RangeValueProvider()
		{
			incrementer = new StringIncrementer();
		}

		public ITerminalRangeInput<Token> CreateTerminalRangeInput(Token FirstValue, Token LastValue)
		{
			return new TerminalRangeInput(FirstValue, LastValue);
		}

		public Token GetNextValue(Token Token)
		{
			if (IsMaxValue(Token)) throw new ArgumentOutOfRangeException("Value");
			if (Token.Value==Token.MaxStringValue)
			{
				return new Token(incrementer.Inc(Token.Class), Token.MinStringValue);
			}
			else
			{
				return new Token(Token.Class, incrementer.Inc(Token.Value));
			}
		}

		public Token GetPreviousValue(Token Token)
		{
			if (IsMinValue(Token)) throw new ArgumentOutOfRangeException("Value");
			if (Token.Value == Token.MinStringValue)
			{
				return new Token(incrementer.Dec(Token.Class), Token.MaxStringValue);
			}
			else
			{
				return new Token(Token.Class, incrementer.Dec(Token.Value));
			}
		}

		public bool IsMaxValue(Token Value)
		{
			return Value == Token.MaxValue;
		}

		public bool IsMinValue(Token Value)
		{
			return Value == Token.MinValue;
		}
	}
}
