using FSMLib.Inputs;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class BasePredicate<T>:IPredicate<T>,IEquatable<BasePredicate<T>>
	{

		public override string ToString()
		{
			return ToString(null);
		}

		

		public abstract string ToString(ISituationPredicate<T> CurrentPredicate);

		public abstract bool Equals(IPredicate<T> other);

		public bool Equals(BasePredicate<T> other)
		{
			return Equals((IPredicate<T>)other);
		}
	}
}
