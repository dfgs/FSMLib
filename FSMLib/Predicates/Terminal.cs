using FSMLib.Inputs;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Terminal<T>: InputPredicate<T>
	{

		public T Value
		{
			get;
			set;
		}

		public override BaseTerminalInput<T> GetInput()
		{
			return new TerminalInput<T>() { Value = Value };
		}


		
		public override string ToString(InputPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{Value}";
			else return Value.ToString();
		}




		public static implicit operator Terminal<T>(T Value)
		{
			return new Terminal<T>() { Value=Value};
		}




	}
}
