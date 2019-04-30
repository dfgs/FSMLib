using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;

namespace FSMLib.SegmentFactories
{
	public class NodeConnector<T> : INodeConnector<T>
	{
		public NodeConnector()
		{
		}

		public void Connect(INodeContainer<T> NodeContainer, IEnumerable<Node<T>> InputNodes, IEnumerable<Node<T>> TargetNodes)
		{
			Transition<T> transition;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (InputNodes == null) throw new ArgumentNullException("InputNodes");
			if (TargetNodes == null) throw new ArgumentNullException("TargetNodes");

			foreach (Node<T> inputNode in InputNodes)
			{
				foreach (Node<T> targetNode in TargetNodes)
				{
					transition = new Transition<T>() { TargetNodeIndex = NodeContainer.GetNodeIndex(targetNode)};
					inputNode.Transitions.Add(transition);
				}
			}
		}


	}
}
