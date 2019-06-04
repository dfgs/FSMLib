using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class Situation<T>:IEquatable<Situation<T>>
	{
		public Rule<T> Rule
		{
			get;
			set;
		}
		public InputPredicate<T> Predicate
		{
			get;
			set;
		}

		public bool CanReduce
		{
			get { return Predicate is ReducePredicate<T>; }
		}

		public BaseTerminalInput<T> Input
		{
			get;
			set;
		}

		public bool Equals(Situation<T> other)
		{
			if (this == other) return true;
			if ((Rule != other.Rule) || (Predicate != other.Predicate)) return false;
			if ((Input == null) && (other.Input == null)) return true;
			return Input.Equals(other.Input);
		}

		public override string ToString()
		{
			return $"{Rule.ToString(Predicate)} ({Input})" ;
		}

		

	}
}
