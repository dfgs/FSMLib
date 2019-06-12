using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public class ReducePredicate<T> : SituationInputPredicate<T>
	{


		

		public ReducePredicate()
		{

		}


		public override IInput<T> GetInput()
		{
			return null;
		}

		public override bool Match(T Input)
		{
			return false;
		}
		public override bool Match(IInput<T> Input)
		{
			return false;
		}

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•←";
			else return "←";
		}

		public override bool Equals(IPredicate<T> other)
		{
			return other is ReducePredicate<T>;
		}


	}



}
