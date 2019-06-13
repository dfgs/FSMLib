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
	public abstract class TerminalRangePredicate<T>: SituationPredicate<T>
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

		public override IInput<T> GetInput()
		{
			return new TerminalRangeInput<T>() {FirstValue=this.FirstValue,LastValue=this.LastValue };
		}

		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•[{FirstValue}-{LastValue}]";
			else return $"[{FirstValue}-{LastValue}]";
		}


		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is TerminalRangePredicate<T> o) ) return false;
			return FirstValue.Equals(o.FirstValue) && (LastValue.Equals(o.LastValue));
		}

		
	}
}
