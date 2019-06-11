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
	public class TerminalRange<T>: SituationPredicate<T>
	{

		[XmlAttribute]
		public T FirstValue
		{
			get;
			set;
		}
		[XmlAttribute]
		public T LastValue
		{
			get;
			set;
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
			return "toto";
			//if (CurrentPredicate == this) return $"•{Value}";
			//else return Value.ToString();
		}


		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is TerminalRange<T> o) ) return false;
			return FirstValue.Equals(o.FirstValue) && (LastValue.Equals(o.LastValue));
		}


	}
}
