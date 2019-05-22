using FSMLib.Graphs.Transitions;
using FSMLib.Rules;
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


		public GraphFactoryContext(ISegmentFactoryProvider<T> SegmentFactoryProvider, Graph<T> Graph)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
	
			this.cache = new Dictionary<Rule<T>, Segment<T>>();

			compilingList = new List<Rule<T>>();
		}


		public Segment<T> BuildSegment( Rule<T> Rule, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			ISegmentFactory<T> segmentFactory;
			Segment<T> segment;
			
			if (cache.TryGetValue(Rule, out segment))
			{
				return segment;
			}

			if (compilingList.Contains(Rule)) throw new InvalidOperationException("Recursive rule call");
			compilingList.Add(Rule);
			segmentFactory = segmentFactoryProvider.GetSegmentFactory(Rule.Predicate);


			segment = segmentFactory.BuildSegment(this, Rule.Predicate,OutTransitions  );
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
					switch(transition)
					{
						case TerminalTransition<T> tr:
							node.TerminalTransitions.Add(tr);
							break;
						case NonTerminalTransition<T> tr:
							node.NonTerminalTransitions.Add(tr);
							break;
						case ReductionTransition<T> tr:
							node.ReductionTransitions.Add(tr);
							break;
						case AcceptTransition<T> tr:
							node.AcceptTransitions.Add(tr);
							break;

						default:
							throw (new NotImplementedException("Invalid transition type"));
					}
					
				}
			}
		}


		public Node<T> GetTargetNode(int Index)
		{
			if ((Index < 0) || (Index >= graph.Nodes.Count)) throw (new IndexOutOfRangeException("Node index is out of range"));
			return graph.Nodes[Index];
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

		public IEnumerable<T> GetAlphabet()
		{
			return graph.Alphabet;
		}

	}
}
