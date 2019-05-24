using FSMLib.ActionTables.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class Segment<T>
	{
		public IEnumerable<BaseAction<T>> Actions
		{
			get;
			set;
		}

		public IEnumerable<Node<T>> Outputs
		{
			get;
			set;
		}

		

		public Segment()
		{
		}


	}
}
