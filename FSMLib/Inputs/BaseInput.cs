using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public abstract class BaseInput<T> : IInput<T>
	{
		public abstract bool Match(IInput<T> Other);
	}
}
