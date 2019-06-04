using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public abstract class BaseInput<T> : IInput<T>,IEquatable<BaseInput<T>>
	{
		public abstract bool Equals(IInput<T> other);
		public abstract bool Equals(BaseInput<T> other);
		public abstract bool Match(IInput<T> Other);
	}
}
