using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public class One<T>:RulePredicate<T>
	{

		public T Value
		{
			get;
			set;
		}

		public override IEnumerable<RulePredicate<T>> Enumerate()
		{
			yield return this;
		}

		public override string ToParenthesisString(RulePredicate<T> Current)
		{
			if (Current == this) return $"{Bullet}{Value.ToString()}";
			return Value.ToString();
		}

		public override string ToString(RulePredicate<T> Current)
		{
			if (Current == this) return $"{Bullet}{Value.ToString()}";
			return Value.ToString();
		}
		public override string ToString()
		{
			return Value.ToString();
		}
		
		
	}
}
