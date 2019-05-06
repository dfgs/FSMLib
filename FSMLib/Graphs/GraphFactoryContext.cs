using FSMLib.SegmentFactories;
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
		private ISegmentFactoryProvider<T> segmentFactoryProvider;

		public GraphFactoryContext(ISegmentFactoryProvider<T> SegmentFactoryProvider, Graph<T> Graph)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			this.graph = Graph;
		}


		public Segment<T> BuildSegment( Rule<T> Rule)
		{
			ISegmentFactory<T> segmentFactory;
			Segment<T> segment;

			segmentFactory = segmentFactoryProvider.GetSegmentFactory(Rule.Predicate);
			segment = segmentFactory.BuildSegment(this, Rule.Predicate, new EORTransition<T>() { Rule = Rule.Name }.AsEnumerable());



			return segment;
		}
		public void Connect(IEnumerable<Node<T>> Nodes, IEnumerable<BaseTransition<T>> Transitions)
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
