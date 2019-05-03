using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;

namespace FSMLib.SegmentFactories
{
	public class NodeConnector<T> : INodeConnector<T>
	{
		public NodeConnector()
		{
		}

		public void Connect( IEnumerable<Node<T>> Nodes, IEnumerable<Transition<T>> Transitions)
		{

			//if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Transitions == null) throw new ArgumentNullException("Transitions");

			foreach (Node<T> node in Nodes)
			{
				foreach (Transition<T> transition in Transitions)
				{
					if (transition == Transition<T>.Termination) node.IsTermination = true;
					else node.Transitions.Add(transition);
				}
			}
		}


	}
}
