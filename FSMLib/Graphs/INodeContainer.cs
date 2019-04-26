using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public interface INodeContainer
	{
		Node GetTargetNode(Transition Transition);
		Node CreateNode();
		int GetNodeIndex(Node Node);
	}
}
