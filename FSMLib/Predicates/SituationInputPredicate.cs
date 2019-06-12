using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class SituationInputPredicate<T>: SituationPredicate<T>,ISituationInputPredicate<T>
	{
		public abstract IInput<T> GetInput();


		public override bool Match(T Input)
		{
			return GetInput().Match(Input);
		}
		public override bool Match(IInput<T> Input)
		{
			return GetInput().Match(Input);
		}
	}
}
