using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables.Actions
{
	public abstract class Shift<T>:BaseAction<T>
	{
		public int TargetNodeIndex
		{
			get;
			set;
		}

	}
}
