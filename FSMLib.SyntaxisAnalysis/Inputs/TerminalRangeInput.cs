using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis.Inputs
{
	public class TerminalRangeInput:ITerminalRangeInput<Token>
	{
		//private static Comparer<Token> comparer = Comparer<Token>.Default;
		public Token FirstValue
		{
			get;
			set;
		}
		public Token LastValue
		{
			get;
			set;
		}

		public TerminalRangeInput(Token FirstValue,Token LastValue)
		{
			
			this.FirstValue = FirstValue;this.LastValue = LastValue;
		}

		public  bool Equals(IInput<Token> other)
		{
			if (other == null) return false;
			if (other is TerminalRangeInput o)
			{
				return o.FirstValue.Equals(this.FirstValue) && o.LastValue.Equals(this.LastValue);
			}
			return false;
		}


		public bool Match(Token Input)
		{
			return (Input.CompareTo(FirstValue)>=0) && (Input.CompareTo(LastValue)<=0);
		}

		public bool Match(IInput<Token> Input)
		{
			switch (Input)
			{
				case TerminalInput terminal:
					return (terminal.Value.CompareTo(FirstValue)>=0) && (terminal.Value.CompareTo(LastValue)<=0);
				case TerminalRangeInput range:
					return (range.FirstValue.CompareTo(FirstValue)>=0) && (range.LastValue.CompareTo(LastValue)<=0);
				default: return false;
			}
		}

		public override string ToString()
		{
			if ((FirstValue == Token.MinValue) && (LastValue == Token.MaxValue)) return ".";
			return $"[{FirstValue}-{LastValue}]";
		}

		

	}
}
