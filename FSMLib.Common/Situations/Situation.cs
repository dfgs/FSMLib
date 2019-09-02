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
		}
		public ISituationPredicate<T> Predicate
		{
			get;
		}

		public bool CanReduce
		{
			get { return Predicate is IReducePredicate<T>; }
		}

		public IReduceInput<T> Input
		{
			get;
		}

		public Situation(IRule<T> Rule,ISituationPredicate<T> Predicate, IReduceInput<T> Input)
		{
			if (Rule == null) throw (new ArgumentNullException("Rule"));
			if (Predicate == null) throw (new ArgumentNullException("Predicate"));
			if (Input == null) throw (new ArgumentNullException("Input"));
			this.Rule = Rule;this.Predicate = Predicate;
			this.Input = Input;
		}


		public bool Equals(ISituation<T> other)
		{
			if (this == other) return true;
			return (Rule.Equals( other.Rule)) && (Predicate.Equals(other.Predicate) && (Input.Equals(other.Input)));
		}

		public override string ToString()
		{
			return $"{Rule.ToString(Predicate)} ({Input})" ;
		}

		

	}
}
