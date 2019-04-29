using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;

namespace FSMLib.SegmentFactories
{
	public class NodeConnector : INodeConnector
	{
		public NodeConnector()
		{
		}

		public void Connect(INodeContainer NodeContainer, IEnumerable<Node> InputNodes, IEnumerable<Node> TargetNodes)
		{
			Transition transition;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (InputNodes == null) throw new ArgumentNullException("InputNodes");
			if (TargetNodes == null) throw new ArgumentNullException("TargetNodes");

			foreach (Node inputNode in InputNodes)
			{
				foreach (Node targetNode in TargetNodes)
				{
					transition = new Transition() { TargetNodeIndex = NodeContainer.GetNodeIndex(targetNode)};
					inputNode.Transitions.Add(transition);
				}
			}
		}


	}
}
