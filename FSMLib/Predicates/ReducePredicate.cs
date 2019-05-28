using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	public class ReducePredicate<T> : InputPredicate<T>
	{
		public static ReducePredicate<T> Instance = new ReducePredicate<T>();

		private static ReduceInput<T> input = new ReduceInput<T>();

		private ReducePredicate()
		{
		}

		public override IInput<T> GetInput()
		{
			return input;
		}

		

		public override string ToString(InputPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•";
			else return "";
		}

		






	}
}
