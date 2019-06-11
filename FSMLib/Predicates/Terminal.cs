using FSMLib.Inputs;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Terminal<T>: SituationInputPredicate<T>
	{

		[XmlAttribute]
		public T Value
		{
			get;
			set;
		}

		public override IEnumerable<IInput<T>> GetInputs()
		{
			yield return new TerminalInput<T>() { Value = Value };
		}
		

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{Value}";
			else return Value.ToString();
		}




		public static implicit operator Terminal<T>(T Value)
		{
			return new Terminal<T>() { Value=Value};
		}

		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is Terminal<T> o) ) return false;
			if (Value == null) return o.Value == null;
			return Value.Equals(o.Value);
		}


	}
}
