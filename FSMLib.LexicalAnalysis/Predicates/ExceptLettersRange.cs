using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class ExceptLettersRange: LexicalPredicate, ISituationPredicate<char>
	{
		[XmlAttribute]
		public char FirstValue
		{
			get;
			set;
		}
		[XmlAttribute]
		public char LastValue
		{
			get;
			set;
		}


		public ExceptLettersRange()
		{
			this.FirstValue = (char)0; this.LastValue = (char)0;
		}
		public ExceptLettersRange(char FirstValue, char LastValue)
		{
			this.FirstValue = FirstValue; this.LastValue = LastValue;
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			if (FirstValue==char.MinValue)
			{
				if (LastValue == char.MaxValue) yield break;
				yield return new LettersRangeInput((char)(LastValue + 1), char.MaxValue);
			}
			else
			{
				if (LastValue == char.MaxValue) yield return new LettersRangeInput(char.MinValue, (char)(FirstValue - 1));
				else
				{
					yield return new LettersRangeInput(char.MinValue, (char)(FirstValue - 1));
					yield return new LettersRangeInput((char)(LastValue + 1), char.MaxValue);
				}
			}
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•![{FirstValue}-{LastValue}]";
			else return $"![{FirstValue}-{LastValue}]";
		}


		public override  bool Equals(IPredicate<char> other)
		{
			if (!(other is ExceptLettersRange o)) return false;
			return ((FirstValue==o.FirstValue) && (LastValue==o.LastValue));
		}

	



	}
}
