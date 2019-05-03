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

		public void Connect( IEnumerable<Node<T>> Nodes, IEnumerable<BaseTransition<T>> Transitions)
		{

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Transitions == null) throw new ArgumentNullException("Transitions");

			foreach (Node<T> node in Nodes)
			{
				foreach (BaseTransition<T> transition in Transitions)
				{
					if (transition is EORTransition<T> eorTransition) node.RecognizedRules.Add(eorTransition.Rule);
					else node.Transitions.Add((Transition<T>)transition);
				}
			}
		}


	}
}
