using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public class ReducePredicate<T> : InputPredicate<T>
	{

		public static ReducePredicate<T> Instance = new ReducePredicate<T>();

		private static ReduceInput<T> input = new ReduceInput<T>();
		public override BaseInput<T> GetInput()
		{
			return input; ;
		}

		private ReducePredicate()
		{

		}

		public override string ToString(InputPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•←";
			else return "←";
		}

	}



}
