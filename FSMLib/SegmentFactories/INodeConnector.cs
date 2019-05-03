using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public interface INodeConnector<T>
	{
		void Connect( IEnumerable<Node<T>> Nodes, IEnumerable<BaseTransition<T>> Transitions);
	}
}
