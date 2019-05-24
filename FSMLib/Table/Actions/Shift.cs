using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table.Actions
{
	public abstract class Shift<T>:BaseAction<T>
	{
		public int TargetStateIndex
		{
			get;
			set;
		}

	}
}
