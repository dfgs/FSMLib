using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class ReduceInput<T>:BaseTerminalInput<T>//,IEquatable<BaseInput<T>>
	{

		public override bool Equals(BaseInput<T> other)
		{
			return other is ReduceInput<T>;
		}
		public override bool Equals(BaseTerminalInput<T> other)
		{
			return other is ReduceInput<T>;
		}
		public override bool Equals(IInput<T> other)
		{
			return other is ReduceInput<T>;
		}

		public override bool Match(IInput<T> Other)
		{
			return false;
		}

		public override bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return "←";
		}

		
	}
}
