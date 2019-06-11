﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public class EOS<T>: SituationInputPredicate<T>
	{
		public override IEnumerable<IInput<T>> GetInputs()
		{
			yield return new EOSInput<T>();
		}


		

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•¤";
			else return "¤";
		}

		public override bool Equals(IPredicate<T> other)
		{
			return other is EOS<T>;
		}







	}
}
