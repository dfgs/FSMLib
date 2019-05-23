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
		private List<string> openList;

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
			openList = new List<string>();
		}


		public Segment<T> BuildSegment(Rule<T> Rule, IEnumerable<BaseTransition<T>> OutTransitions)
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


			segment = segmentFactory.BuildSegment(this, Rule.Predicate, OutTransitions);
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
					switch (transition)
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


		public IEnumerable<T> GetFirstTerminalsAfterTransition(IEnumerable<Rule<T>> Rules, NonTerminalTransition<T> NonTerminalTransition)
		{
			Node<T> node;
			List<T> items;

			items = new List<T>();

			node = GetTargetNode(NonTerminalTransition.TargetNodeIndex);
			foreach(TerminalTransition<T> transition in node.TerminalTransitions)
			{
				if (!items.Contains(transition.Value)) items.Add(transition.Value);
			}
			foreach(NonTerminalTransition<T> nonTerminalTransition in node.NonTerminalTransitions)
			{
				foreach(T item in GetFirstTerminalsForRule(Rules,nonTerminalTransition.Name))
				{
					if (!items.Contains(item)) items.Add(item);
				}
			}

			return items;
		}

		public IEnumerable<T> GetFirstTerminalsForRule(IEnumerable<Rule<T>> Rules,string Name)
		{
			Segment<T> segment;
			List<T> items;

			items = new List<T>();

			if (openList.Contains(Name)) return items;
			openList.Add(Name);
			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{
	
				segment = BuildSegment(rule, Enumerable.Empty<BaseTransition<T>>());

				foreach (TerminalTransition<T> transition in segment.Inputs.OfType<TerminalTransition<T>>())
				{
					if (!items.Contains(transition.Value)) items.Add(transition.Value);
				}

				foreach (NonTerminalTransition<T> transition in segment.Inputs.OfType<NonTerminalTransition<T>>())
				{
					foreach(T input in GetFirstTerminalsForRule(Rules,transition.Name))
					{
						if (!items.Contains(input)) items.Add(input);
					}
				}

			}
			openList.Remove(Name);

			return items;
		}

		public IEnumerable<Segment<T>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<T>> Rules, string Name)
		{
			Segment<T> segment;

			if (openList.Contains(Name)) yield break;
			openList.Add(Name);

			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{

				segment = BuildSegment(rule, Enumerable.Empty<BaseTransition<T>>());

				yield return segment;
				
				foreach (NonTerminalTransition<T> nonTerminalTransition in segment.Inputs.OfType<NonTerminalTransition<T>>())
				{
					foreach (Segment<T> nestedSegment in GetDeveloppedSegmentsForRule(Rules, nonTerminalTransition.Name))
					{
						yield return nestedSegment;
					}
				}

			}
			openList.Remove(Name);


		}


	}
}
