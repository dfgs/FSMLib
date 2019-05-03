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

		public void Connect(string Rule, IEnumerable<Node<T>> Nodes, IEnumerable<Transition<T>> Transitions)
		{

			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Transitions == null) throw new ArgumentNullException("Transitions");

			foreach (Node<T> node in Nodes)
			{
				foreach (Transition<T> transition in Transitions)
				{
					if (transition == Transition<T>.Termination) node.RecognizedRules.Add(Rule);
					else node.Transitions.Add(transition);
				}
			}
		}


	}
}
