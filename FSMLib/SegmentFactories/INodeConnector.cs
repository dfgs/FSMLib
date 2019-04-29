using FSMLib.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public interface INodeConnector
	{
		void Connect(INodeContainer NodeContainer, IEnumerable<Node> InputNodes, IEnumerable<Node> TargetNodes);
	}
}
