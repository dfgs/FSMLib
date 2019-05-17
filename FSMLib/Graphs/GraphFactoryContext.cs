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
		private List<Rule<T>> compilingList;

		private Graph<T> graph;
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private Dictionary<Rule<T>, Segment<T>> cache;

		/*public IEnumerable<Rule<T>> Rules
		{
			get;
			private set;
		}*/

		public GraphFactoryContext(ISegmentFactoryProvider<T> SegmentFactoryProvider, Graph<T> Graph)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
			/*if (Rules == null) throw new ArgumentNullException("Rules");
			this.Rules = Rules;*/

			this.cache = new Dictionary<Rule<T>, Segment<T>>();

			compilingList = new List<Rule<T>>();
		}


		public Segment<T> BuildSegment( Rule<T> Rule)
		{
			ISegmentFactory<T> segmentFactory;
			Segment<T> segment;
			MatchedRule matchedRule;

			if (cache.TryGetValue(Rule, out segment)) return segment;

			if (compilingList.Contains(Rule)) throw new InvalidOperationException("Recursive rule call");
			compilingList.Add(Rule);
			segmentFactory = segmentFactoryProvider.GetSegmentFactory(Rule.Predicate);
			matchedRule = new MatchedRule() { Name=Rule.Name, ID= Rule.GetHashCode()};
			segment = segmentFactory.BuildSegment(this, Rule.Predicate, new EORTransition<T>() { MatchedRule = matchedRule }.AsEnumerable());
			cache.Add(Rule, segment);
			compilingList.Remove(Rule);

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
					if (transition is EORTransition<T> eorTransition) node.MatchedRules.Add(eorTransition.MatchedRule);
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
