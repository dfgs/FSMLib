using FSMLib.Predicates;
using FSMLib.SyntaxisAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	[Serializable]
	public abstract class SyntaxicPredicate : IPredicate<Token>
	{
		public abstract bool Equals(IPredicate<Token> other);
		public abstract string ToString(ISituationPredicate<Token> CurrentPredicate);
	}
}
