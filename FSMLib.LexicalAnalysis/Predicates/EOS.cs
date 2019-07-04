using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public class EOS: IEOSPredicate<char>
	{
		public  IInput<char> GetInput()
		{
			return new EOSInput<char>();
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public  string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•¤";
			else return "¤";
		}

		public  bool Equals(IPredicate<char> other)
		{
			return other is EOS;
		}


		



	}
}
