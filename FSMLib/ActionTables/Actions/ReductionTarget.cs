using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables.Actions
{
	public class ReductionTarget<T>
	{
		public int TargetNodeIndex
		{
			get;
			set;
		}
		public T Value
		{
			get;
			set;
		}

	}
}
