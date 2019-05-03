using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public interface IInput<T>:IEquatable<IInput<T>>
	{
		int Priority
		{
			get;
		}

		bool Match(IInput<T> Other);
		bool Match(T Other);
	}
}
