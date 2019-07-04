using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public class Reduce : IReducePredicate<char>
	{

		public Reduce()
		{

		}


		public IInput<char> GetInput()
		{
			return null;
		}

		public override string ToString()
		{
			return ToString(null);
		}


		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•←";
			else return "←";
		}

		public bool Equals(IPredicate<char> other)
		{
			return other is Reduce;
		}

		public bool Match(char Input)
		{
			return false;
		}


	}



}
