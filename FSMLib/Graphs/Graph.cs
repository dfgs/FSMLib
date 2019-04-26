using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Graph:INodeContainer
	{
		public List<Node> Nodes
		{
			get;
			set;
		}
		public Graph()
		{
			Nodes = new List<Node>();
		}

		public Node GetTargetNode(Transition Transition)
		{
			if ((Transition.TargetNodeIndex < 0) || (Transition.TargetNodeIndex >= Nodes.Count)) throw (new IndexOutOfRangeException("Node index is out of range"));
			return Nodes[Transition.TargetNodeIndex];
		}

		public Node CreateNode()
		{
			Node node;
			node = new Node();
			Nodes.Add(node);
			return node;
		}

		public int GetNodeIndex(Node Node)
		{
			return Nodes.IndexOf(Node);
		}
	}
}
