using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Transitions
{
	public class ReductionTransition<T>:BaseTransition<T>
	{

		
		public string Name
		{
			get;
			set;
		}

		
		
		public List<ReductionTarget<T>> Targets
		{
			get;
			set;
		}

		public ReductionTransition()
		{
			Targets = new List<ReductionTarget<T>>();
		}

		


	}
}
