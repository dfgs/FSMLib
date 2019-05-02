using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Graph<T> : INodeContainer<T>
	{
		public List<Node<T>> Nodes
		{
			get;
			set;
		}
		public Graph()
		{
			Nodes = new List<Node<T>>();
		}

		public Node<T> GetTargetNode(Transition<T> Transition)
		{
			if ((Transition.TargetNodeIndex < 0) || (Transition.TargetNodeIndex >= Nodes.Count)) throw (new IndexOutOfRangeException("Node index is out of range"));
			return Nodes[Transition.TargetNodeIndex];
		}

		public Node<T> CreateNode()
		{
			Node<T> node;
			node = new Node<T>();
			node.Name = Nodes.Count.ToString();
			Nodes.Add(node);
			return node;
		}

		public int GetNodeIndex(Node<T> Node)
		{
			return Nodes.IndexOf(Node);
		}
	}
}
