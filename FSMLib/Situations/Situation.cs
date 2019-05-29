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
			get;
			set;
		}

		public NonTerminal<T> ParentPredicate
		{
			get;
			set;
		}

		public bool Equals(Situation<T> other)
		{
			return (Rule == other.Rule) && (Predicate == other.Predicate);
		}

		public override string ToString()
		{
			return Rule.ToString(Predicate) ;
		}

		

	}
}
