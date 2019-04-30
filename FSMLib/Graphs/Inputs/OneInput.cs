using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class OneInput<T>:BaseInput<T>
	{
		public T Value
		{
			get;
			set;
		}

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;

			if (Other is OneInput<T> other)
			{
				if (Value == null) return other.Value == null;
				return Value.Equals(other.Value);
			}
			
			return false;
		}

		public override bool Match(T Other)
		{
			if (Other == null) return false;

			if (Value == null) return Other == null;
			return Value.Equals(Other);
		}


	}
}
