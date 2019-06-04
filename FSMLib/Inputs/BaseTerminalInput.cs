using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public abstract class BaseTerminalInput<T>:BaseInput<T>,IEquatable<BaseTerminalInput<T>>
	{
		public abstract bool Equals(BaseTerminalInput<T> other);

		public abstract bool Match(T Value);
	}
}
