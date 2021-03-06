﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Common.Inputs;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;

namespace FSMLib.LexicalAnalysis.Predicates
{
	[Serializable]
	public class EOS: LexicalPredicate, IEOSPredicate<char>
	{
		public  IEnumerable< IInput<char>> GetInputs()
		{
			yield return new EOSInput<char>();
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•¤";
			else return "¤";
		}

		public override bool Equals(IPredicate<char> other)
		{
			return other is EOS;
		}

	
	}
}
