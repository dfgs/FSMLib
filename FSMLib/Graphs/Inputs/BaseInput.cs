using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public abstract class BaseInput<T> : IInput<T>
	{
		public abstract int Priority
		{
			get;
		}

		public abstract bool Equals(IInput<T> other);
		public abstract bool Match(IInput<T> Other);
		public abstract bool Match(T Other);

		
	}
}
