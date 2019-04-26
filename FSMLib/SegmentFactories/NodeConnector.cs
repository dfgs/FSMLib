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
		private INodeContainer nodeContainer;
		public NodeConnector(INodeContainer NodeContainer)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			this.nodeContainer = NodeContainer;
		}

		public void Connect(IEnumerable<Node> InputNodes, IEnumerable<Node> TargetNodes)
		{
			Transition transition;

			foreach (Node inputNode in InputNodes)
			{
				foreach (Node targetNode in TargetNodes)
				{
					transition = new Transition() { TargetNodeIndex = nodeContainer.GetNodeIndex(targetNode)};
					inputNode.Transitions.Add(transition);
				}
			}
		}


	}
}
