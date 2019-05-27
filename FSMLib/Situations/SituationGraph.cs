using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraph<T>:ISituationGraph<T>
	{
		private List<SituationNode<T>> nodes;


		public SituationGraph(IEnumerable<BasePredicate<T>> Predicates)
		{
			SituationNode<T> reduceNode;
			SituationEdge<T> reduceEdge;

			if (Predicates == null) throw new ArgumentNullException("Predicates");
			this.nodes = new List<SituationNode<T>>();

			foreach(BasePredicate<T> predicate in Predicates)
			{
				reduceNode = CreateNode(new ReducePredicate<T>());
				reduceEdge = new SituationEdge<T>();
				reduceEdge.NextPredicate = reduceNode.Predicate;

				BuildPredicate(predicate, reduceEdge.AsEnumerable());
			}
		}

		public IEnumerable<InputPredicate<T>> GetNextPredicates(InputPredicate<T> CurrentPredicate)
		{
			SituationNode<T> node;

			node = nodes.FirstOrDefault(item => item.Predicate == CurrentPredicate);
			if (node == null) return Enumerable.Empty<InputPredicate<T>>();

			return node.Edges.Select(item => item.NextPredicate);
		}

		public bool Contains(InputPredicate<T> Predicate)
		{
			SituationNode<T> node;

			node = nodes.FirstOrDefault(item => item.Predicate == Predicate);
			return (node != null);
		}

		private SituationNode<T> CreateNode(InputPredicate<T> Predicate)
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			node.Predicate = Predicate;
			nodes.Add(node);

			return node;
		}

		private SituationGraphSegment<T> BuildPredicate(BasePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			switch (Predicate)
			{
				case Terminal<T> predicate: return BuildPredicate(predicate,Edges);
				case NonTerminal<T> predicate: return BuildPredicate(predicate, Edges);
				case AnyTerminal<T> predicate: return BuildPredicate(predicate, Edges);
				case Sequence<T> predicate: return BuildPredicate(predicate, Edges);
				case Or<T> predicate: return BuildPredicate(predicate, Edges);
				case OneOrMore<T> predicate: return BuildPredicate(predicate, Edges);
				case ZeroOrMore<T> predicate: return BuildPredicate(predicate, Edges);
				case Optional<T> predicate: return BuildPredicate(predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

		private SituationGraphSegment<T> BuildPredicate(InputPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			node = CreateNode(Predicate);
			node.Edges.AddRange(Edges);

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

			foreach(SituationNode<T> node in itemSegment.OutputNodes)
			{
				node.Edges.AddRange(itemSegment.InputEdges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Predicate.Item, Edges);

			foreach (SituationNode<T> node in itemSegment.OutputNodes)
			{
				node.Edges.AddRange(itemSegment.InputEdges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}


	}
}
