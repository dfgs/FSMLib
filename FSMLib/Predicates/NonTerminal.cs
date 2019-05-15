using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public class NonTerminal<T>:BasePredicate<T>
	{

		public string Name
		{
			get;
			set;
		}

		public override IEnumerable<BasePredicate<T>> Enumerate()
		{
			yield return this;
		}

		
		public override string ToParenthesisString()
		{
			return $"{{{Name}}}";
		}
		public override string ToString()
		{
			return $"{{{Name}}}";
		}








	}
}
