using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class SituationPredicate<T>: BasePredicate<T>,ISituationPredicate<T>
	{
		public abstract IEnumerable<IInput<T>> GetInputs();
		public bool Match(T Input)
		{
			return GetInputs().FirstOrDefault(item => item.Match(Input)) != null;
		}
		public bool Match(IInput<T> Input)
		{
			return GetInputs().FirstOrDefault(item => item.Match(Input)) != null;
		}


	}
}
