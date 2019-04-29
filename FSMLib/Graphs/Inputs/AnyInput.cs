using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class AnyInput<T> : BaseInput<T>
	{
		public override bool Match(IInput<T> Other)
		{
			return Other != null;
		}
	}
}
