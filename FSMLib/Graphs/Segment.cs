using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Segment
	{
		public IEnumerable<Node> Inputs
		{
			get;
			set;
		}

		public IEnumerable<Node> Outputs
		{
			get;
			set;
		}

		

		public Segment()
		{
		}


	}
}
