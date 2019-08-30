using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class AnyLetter : LexicalPredicate, ISituationPredicate<char>
	{
		public IEnumerable<IInput<char>> GetInputs()
		{
			yield return new LettersRangeInput(char.MinValue,char.MaxValue);
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•.";
			else return ".";
		}

		public override bool Equals(IPredicate<char> other)
		{
			return other is AnyLetter;
		}

		



	}

}