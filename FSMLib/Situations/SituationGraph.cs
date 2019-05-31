using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraph<T>:ISituationGraph<T>
	{
		private List<SituationNode<T>> inputPredicateNodes;
		private List<SituationNode<T>> rootPredicateNodes;
		

		public SituationGraph(IEnumerable<Rule<T>> Rules)
		{
			SituationNode<T> rootPredicateNode;
			SituationGraphSegment<T> segment;
			Sequence<T> predicate;

			if (Rules == null) throw new ArgumentNullException("Rules");

			this.inputPredicateNodes = new List<SituationNode<T>>();
			this.rootPredicateNodes = new List<SituationNode<T>>();

			foreach(Rule<T> rule in Rules)
			{
				predicate = new Sequence<T>();
				predicate.Items.Add(rule.Predicate);
				predicate.Items.Add(new ReducePredicate<T>());
				segment=BuildPredicate(predicate, Enumerable.Empty<SituationEdge<T>>() );
				rootPredicateNode = new SituationNode<T>();
				rootPredicateNode.Predicate = rule.Predicate;
				Connect(rootPredicateNode.AsEnumerable(), segment.InputEdges);
				rootPredicateNodes.Add(rootPredicateNode);
			}
		}

		public IEnumerable<InputPredicate<T>> GetNextPredicates(InputPredicate<T> CurrentPredicate)
		{
			SituationNode<T> node;

			node = inputPredicateNodes.FirstOrDefault(item => item.Predicate == CurrentPredicate);
			if (node == null) return Enumerable.Empty<InputPredicate<T>>();

			return node.Edges.Select(item => item.NextPredicate);
		}
		public IEnumerable<InputPredicate<T>> GetRootInputPredicates(BasePredicate<T> RootPredicate)
		{
			SituationNode<T> node;

			node = rootPredicateNodes.FirstOrDefault(item => item.Predicate == RootPredicate);
			if (node == null) return Enumerable.Empty<InputPredicate<T>>();

			return node.Edges.Select(item => item.NextPredicate);
		}

		public bool Contains(InputPredicate<T> Predicate)
		{
			SituationNode<T> node;

			node = inputPredicateNodes.FirstOrDefault(item => item.Predicate == Predicate);
			return (node != null);
		}
		public bool CanReduce(InputPredicate<T> CurrentPredicate)
		{
			return CurrentPredicate is ReducePredicate<T>;
			
		}

		

		private SituationNode<T> CreateNode(InputPredicate<T> Predicate)
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			node.Predicate = Predicate;
			inputPredicateNodes.Add(node);

			return node;
		}

		private SituationGraphSegment<T> BuildPredicate(BasePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			switch (Predicate)
			{
				case Terminal<T> predicate: return BuildPredicate(predicate,Edges);
				case NonTerminal<T> predicate: return BuildPredicate(predicate, Edges);
				case AnyTerminal<T> predicate: return BuildPredicate(predicate, Edges);
				case EOS<T> predicate: return BuildPredicate(predicate, Edges);
				case ReducePredicate<T> predicate: return BuildPredicate(predicate, Edges);
				case Sequence<T> predicate: return BuildPredicate(predicate, Edges);
				case Or<T> predicate: return BuildPredicate(predicate, Edges);
				case OneOrMore<T> predicate: return BuildPredicate(predicate, Edges);
				case ZeroOrMore<T> predicate: return BuildPredicate(predicate, Edges);
				case Optional<T> predicate: return BuildPredicate(predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}
		private void Connect(IEnumerable<SituationNode<T>> Nodes,IEnumerable<SituationEdge<T>> Edges)
		{
			foreach (SituationNode<T> node in Nodes)
			{
				foreach (SituationEdge<T> edge in Edges)
				{
					//if (edge.NextPredicate is ReducePredicate<T>) continue;
					node.Edges.Add(edge);
				}
			}

		}
		private SituationGraphSegment<T> BuildPredicate(InputPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			node = CreateNode(Predicate);
			Connect(node.AsEnumerable(), Edges);

			edge = new SituationEdge<T>();
			edge.NextPredicate = Predicate;

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edge.AsEnumerable();

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			IEnumerable<SituationEdge<T>>  nextEdges;
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			nextEdges = Edges;
			for (int t = Predicate.Items.Count - 1; t >= 0; t--)
			{
				segments[t] = BuildPredicate(Predicate.Items[t], nextEdges);
				nextEdges = segments[t].InputEdges;
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments[0].InputEdges;
			segment.OutputNodes = segments[Predicate.Items.Count - 1].OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			for (int t = 0; t <Predicate.Items.Count; t++)
			{
				segments[t] = BuildPredicate(Predicate.Items[t], Edges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments.SelectMany(item=>item.InputEdges);
			segment.OutputNodes = segments.SelectMany(item => item.OutputNodes);

			return segment;
		}


		private SituationGraphSegment<T> BuildPredicate(Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment,segment;

			itemSegment = BuildPredicate(Predicate.Item, Edges);
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment,segment;

			itemSegment = BuildPredicate(Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes,itemSegment.InputEdges);

			
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}


	}
}
