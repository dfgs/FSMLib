using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public class ReductionTarget<T>
	{
		public int TargetStateIndex
		{
			get;
			set;
		}
		public BaseTerminalInput<T> Input
		{
			get;
			set;
		}

	}
}
