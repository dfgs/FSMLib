using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	[Serializable]
	public class EOS: SyntaxicPredicate, IEOSPredicate<Token>
	{
		public  IEnumerable< IInput<Token>> GetInputs()
		{
			yield return new EOSInput<Token>();
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•¤";
			else return "¤";
		}

		public override bool Equals(IPredicate<Token> other)
		{
			return other is EOS;
		}

	
	}
}
