using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Predicates
{
	[Serializable]
	public abstract class LexicalPredicate : IPredicate<char>
	{
		public abstract bool Equals(IPredicate<char> other);
		public abstract string ToString(ISituationPredicate<char> CurrentPredicate);
	}
}
