using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Transitions
{
	public class ShiftTransition<T>:BaseTransition<T>
	{
		public int TargetNodeIndex
		{
			get;
			set;
		}

	}
}
