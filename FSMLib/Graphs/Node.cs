using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Node
	{
		public List<Transition> Transitions
		{
			get;
			set;
		}

		public Node()
		{
			Transitions = new List<Transition>();
		}

	}
}
