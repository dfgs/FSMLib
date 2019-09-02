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
	public interface ISituation<T>: IEquatable<ISituation<T>>
	{
		IRule<T> Rule
		{
			get;
		}
		ISituationPredicate<T> Predicate
		{
			get;
		}
		IReduceInput<T> Input
		{
			get;
		}
		bool CanReduce
		{
			get;
		}

		
	}
}
