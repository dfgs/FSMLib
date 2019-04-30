using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public interface INodeContainer<T>
	{
		Node<T> GetTargetNode(Transition<T> Transition);
		Node<T> CreateNode();
		int GetNodeIndex(Node<T> Node);
	}
}
