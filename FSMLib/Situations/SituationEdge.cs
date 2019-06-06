using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public  class SituationEdge<T>
	{
		public Rule<T> Rule
		{
			get;
			set;
		}

		public SituationPredicate<T> Predicate
		{
			get;
			set;
		}

		public SituationNode<T> TargetNode
		{
			get;
			set;
		}


	}
}
