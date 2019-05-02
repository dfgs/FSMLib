using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Any<T>:RulePredicate<T>
	{

		
		public override IEnumerable<RulePredicate<T>> Enumerate()
		{
			yield return this;
		}

		
		public override string ToParenthesisString()
		{
			return ".";
		}
		public override string ToString()
		{
			return ".";
		}



		




	}
}
