using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class AnyTerminal : SyntaxicPredicate, ISituationPredicate<Token>
	{
		public IEnumerable<IInput<Token>> GetInputs()
		{
			yield return new TerminalRangeInput(Token.MinValue, Token.MaxValue);
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•.";
			else return ".";
		}

		public override bool Equals(IPredicate<Token> other)
		{
			return other is AnyTerminal;
		}

		



	}

}