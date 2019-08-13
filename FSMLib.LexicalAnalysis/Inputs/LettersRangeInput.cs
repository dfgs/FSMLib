using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Inputs
{
	public class LettersRangeInput:IInput<char>, IReduceInput<char>
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


		public bool Match(char Input)
		{
			return (Input >= FirstValue) && (Input <= LastValue);
		}

		public bool Match(IInput<char> Input)
		{
			switch (Input)
			{
				case LetterInput terminal:
					return (terminal.Value >= FirstValue) && (terminal.Value <= LastValue);
				case LettersRangeInput range:
					return (range.FirstValue >= FirstValue) && (range.LastValue <= LastValue);
				default: return false;
			}
		}

		public override string ToString()
		{
			if ((FirstValue == char.MinValue) && (LastValue == char.MaxValue)) return ".";
			return $"[{FirstValue}-{LastValue}]";
		}

		

	}
}
