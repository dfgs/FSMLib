using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public interface IPredicate<T>:IEquatable<IPredicate<T>>
	{
		string ToString(ISituationPredicate<T> CurrentPredicate);
	}
}
