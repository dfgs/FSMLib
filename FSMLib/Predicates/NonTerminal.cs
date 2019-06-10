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
	public class NonTerminal<T>:SituationPredicate<T>
	{

		public string Name
		{
			get;
			set;
		}




		
		public override IEnumerable<IInput<T>> GetInputs()
		{
			yield return new NonTerminalInput<T>() { Name = this.Name };
		}

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{{{Name}}}";
			else return $"{{{Name}}}";
		}


		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is NonTerminal<T> o)) return false;
			if (Name == null) return o.Name == null;
			return Name==o.Name;
		}





	}
}
