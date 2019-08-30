using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationEdge<T>
	{
		IRule<T> Rule
		{
			get;
		}

		ISituationPredicate<T> Predicate
		{
			get;
		}

		ISituationNode<T> TargetNode
		{
			get;
		}
	}
}
