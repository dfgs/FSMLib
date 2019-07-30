using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
    public interface IRule<T>:IEquatable<IRule<T>>
    {
		string Name
		{
			get;
		}

		bool IsAxiom
		{
			get;
		}
		
		IPredicate<T> Predicate
		{
			get;
		}



		string ToString(ISituationPredicate<T> CurrentPredicate);
		

		


	}
}
