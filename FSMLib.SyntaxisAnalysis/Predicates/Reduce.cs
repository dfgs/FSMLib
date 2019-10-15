using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.SyntaxicAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class Reduce : SyntaxicPredicate, IReducePredicate<Token>
	{

		public Reduce()
		{

		}


		public IEnumerable<IInput<Token>> GetInputs()
		{
			yield return new EOSInput<Token>();// break;
		}

		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•;";
			else return ";";
		}

		public override bool Equals(IPredicate<Token> other)
		{
			return other is Reduce;
		}

	

	}



}
