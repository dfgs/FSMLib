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
		}

		public ISituationPredicate<T> Predicate
		{
			get;
		}

		public ISituationNode<T> TargetNode
		{
			get;
		}

		public SituationEdge(IRule<T> Rule, ISituationPredicate<T> Predicate, ISituationNode<T> TargetNode)
		{
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (TargetNode == null) throw new ArgumentNullException("TargetNode");

			this.Rule = Rule;this.Predicate = Predicate;this.TargetNode = TargetNode;
		}

	}
}
