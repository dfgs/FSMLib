using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	[Serializable]
	public class Terminal<T>:BasePredicate<T>
	{

		public T Value
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
			return Value.ToString();
		}
		public override string ToString()
		{
			return Value.ToString();
		}


		
		public static implicit operator Terminal<T>(T Value)
		{
			return new Terminal<T>() { Value=Value};
		}




	}
}
