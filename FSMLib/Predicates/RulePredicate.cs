using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class RulePredicate<T>
	{
		public static readonly char Bullet = '•';


		public abstract IEnumerable<RulePredicate<T>> Enumerate();

		public abstract string ToParenthesisString(RulePredicate<T> Current);
		public abstract string ToString(RulePredicate<T> Current);


	}
}
