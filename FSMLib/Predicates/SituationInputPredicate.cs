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
		public abstract IEnumerable<IInput<T>> GetInputs();


		public override bool Match(T Input)
		{
			return GetInputs().FirstOrDefault(item => item.Match(Input)) != null;
		}
		public override bool Match(IInput<T> Input)
		{
			return GetInputs().FirstOrDefault(item => item.Match(Input)) != null;
		}
	}
}
