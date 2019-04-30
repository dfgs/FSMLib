using FSMLib.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public class Situation<T>:IEquatable<Situation<T>>
	{
		public Graph<T> Graph
		{
			get;
			set;
		}

		public int NodeIndex
		{
			get;
			set;
		}

		public bool Equals(Situation<T> other)
		{
			if (other == null) return false;
			return ((other.Graph == Graph) && (other.NodeIndex == NodeIndex));
		}

	}
}
