using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Situations
{
	public class Situation<T>:ISituation<T>
	{
		public IRule<T> Rule
		{
			get;
			set;
		}
		public ISituationPredicate<T> Predicate
		{
			get;
			set;
		}

		public bool CanReduce
		{
			get { return Predicate is IReducePredicate<T>; }
		}

		public IReduceInput<T> Input
		{
			get;
			set;
		}



		public bool Equals(ISituation<T> other)
		{
			if (this == other) return true;
			if ((Rule != other.Rule) || (Predicate != other.Predicate)) return false;
			if (Input == null) return (other.Input == null);
			return Input.Equals(other.Input);
		}

		public override string ToString()
		{
			return $"{Rule.ToString(Predicate)} ({Input})" ;
		}

		

	}
}
