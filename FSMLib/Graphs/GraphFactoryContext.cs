using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class GraphFactoryContext<T> : IGraphFactoryContext<T>
	{
		private Graph<T> graph;

		public GraphFactoryContext(Graph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
		}

		public Node<T> GetTargetNode(Transition<T> Transition)
		{
			if ((Transition.TargetNodeIndex < 0) || (Transition.TargetNodeIndex >= graph.Nodes.Count)) throw (new IndexOutOfRangeException("Node index is out of range"));
			return graph.Nodes[Transition.TargetNodeIndex];
		}

		public Node<T> CreateNode()
		{
			Node<T> node;
			node = new Node<T>();
			node.Name = graph.Nodes.Count.ToString();
			graph.Nodes.Add(node);
			return node;
		}

		public int GetNodeIndex(Node<T> Node)
		{
			return graph.Nodes.IndexOf(Node);
		}
	}
}
