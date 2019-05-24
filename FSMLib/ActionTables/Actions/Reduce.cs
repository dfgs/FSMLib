using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables.Actions
{
	public class Reduce<T>:BaseAction<T>
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

		public Reduce()
		{
			Targets = new List<ReductionTarget<T>>();
		}

		


	}
}
