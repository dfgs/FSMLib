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
	public  class SituationEdge<T>:ISituationEdge<T>
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

		public ISituationNode<T> TargetNode
		{
			get;
			set;
		}

		
	}
}
