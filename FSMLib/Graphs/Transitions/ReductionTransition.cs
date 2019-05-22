using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Transitions
{
	public class ReductionTransition<T>:BaseTransition<T>
	{

		public T Value
		{
			get;
			set;
		}

		public int TargetNodeIndex
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public bool IsAxiom
		{
			get;
			set;
		}

		public ReductionTransition()
		{

		}

		


	}
}
