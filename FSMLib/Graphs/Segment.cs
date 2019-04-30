using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Segment<T>
	{
		public IEnumerable<Node<T>> Inputs
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
