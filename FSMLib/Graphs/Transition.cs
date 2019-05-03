using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Transition<T>
	{
		public static Transition<T> Termination = new Transition<T>() { Input=null,TargetNodeIndex=-1 };

		public int TargetNodeIndex
		{
			get;
			set;
		}
		public IInput<T> Input
		{
			get;
			set;
		}
	}
}
