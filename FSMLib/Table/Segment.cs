using FSMLib.Table.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class Segment<T>
	{
		public IEnumerable<BaseAction<T>> Actions
		{
			get;
			set;
		}

		public IEnumerable<State<T>> Outputs
		{
			get;
			set;
		}

		

		public Segment()
		{
		}


	}
}
