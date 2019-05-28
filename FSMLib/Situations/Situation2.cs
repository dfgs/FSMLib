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
	public class Situation2<T>:IEquatable<Situation2<T>>
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

		public NonTerminal<T> ParentPredicate
		{
			get;
			set;
		}

		public bool Equals(Situation2<T> other)
		{
			return (Rule == other.Rule) && (Predicate == other.Predicate);
		}

		public override string ToString()
		{
			return Rule.ToString(Predicate) ;
		}

		

	}
}
