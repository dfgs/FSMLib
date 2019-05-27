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
	public class NonTerminal<T>:InputPredicate<T>
	{

		public string Name
		{
			get;
			set;
		}




		
		public override IInput<T> GetInput()
		{
			return new NonTerminalInput<T>() { Name = this.Name };
		}

		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{{{Name}}}";
			else return $"{{{Name}}}";
		}








	}
}
