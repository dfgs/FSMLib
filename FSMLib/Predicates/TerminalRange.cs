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
		private static Comparer<T> comparer = Comparer<T>.Default;

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
			if (comparer == null) return false;

			return ((comparer.Compare(Input,FirstValue) >= 0) && (comparer.Compare(Input, LastValue) <= 0));
		}
		public override bool Match(IInput<T> Input)
		{
			if (!(Input is TerminalInput<T> terminal)) return false;
			return Match(terminal.Value);
		}


		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•[{FirstValue}-{LastValue}]";
			else return $"[{FirstValue}-{LastValue}]";
		}


		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is TerminalRange<T> o) ) return false;
			return FirstValue.Equals(o.FirstValue) && (LastValue.Equals(o.LastValue));
		}


	}
}
