using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraphSegmentFactory<T>: ISituationGraphSegmentFactory<T>
	{

		public SituationGraphSegmentFactory()
		{

		}

		private SituationNode<T> CreateNode(List<SituationNode<T>> Nodes)
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			Nodes.Add(node);

			return node;
		}
		private SituationEdge<T> CreateEdgeTo(SituationNode<T> Node, Rule<T> Rule, SituationInputPredicate<T> Predicate)
		{
			SituationEdge<T> edge;

			edge = new SituationEdge<T>();
			edge.Rule = Rule;
			edge.Predicate = Predicate;
			edge.TargetNode = Node;

			return edge;
		}
		private void Connect(IEnumerable<SituationNode<T>> Nodes, IEnumerable<SituationEdge<T>> Edges)
		{
			foreach (SituationNode<T> node in Nodes)
			{
				foreach (SituationEdge<T> edge in Edges)
				{
					node.Edges.Add(edge);
				}
			}

		}


		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, IPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");


			switch (Predicate)
			{
				case Terminal<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case NonTerminal<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case AnyTerminal<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case TerminalRange<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case EOS<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case ReducePredicate<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case Sequence<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case Or<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case OneOrMore<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case ZeroOrMore<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				case Optional<T> predicate: return BuildSegment(Nodes, Rule, Alphabet, predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, SituationInputPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			node = CreateNode(Nodes);
			Connect(node.AsEnumerable(), Edges);

			edge = CreateEdgeTo(node, Rule, Predicate);

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edge.AsEnumerable();

			return segment;
		}
		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, AnyTerminal<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			List<SituationEdge<T>> edges;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			node = CreateNode(Nodes);
			Connect(node.AsEnumerable(), Edges);

			edges = new List<SituationEdge<T>>();
			foreach (T item in Alphabet)
			{
				edge = CreateEdgeTo(node, Rule, new Terminal<T>() { Value=item } );
				edges.Add(edge);
			}

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edges;

			return segment;
		}
		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, TerminalRange<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			List<SituationEdge<T>> edges;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;
			Comparer<T> comparer;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			comparer = Comparer<T>.Default;
			if (comparer == null) throw new ArgumentNullException($"No default comparer found for type {typeof(T).Name}");

			node = CreateNode(Nodes);
			Connect(node.AsEnumerable(), Edges);
			
			edges = new List<SituationEdge<T>>();
			foreach (T item in Alphabet)
			{
				if ((comparer.Compare(item, Predicate.FirstValue) >= 0) && (comparer.Compare(item, Predicate.LastValue) <= 0))
				{
					edge = CreateEdgeTo(node, Rule, new Terminal<T>() { Value = item });
					edges.Add(edge);
				}
			}

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edges;

			return segment;
		}
		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			IEnumerable<SituationEdge<T>> nextEdges;
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			nextEdges = Edges;
			for (int t = Predicate.Items.Count - 1; t >= 0; t--)
			{
				segments[t] = BuildSegment(Nodes, Rule, Alphabet, Predicate.Items[t], nextEdges);
				nextEdges = segments[t].InputEdges;
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments[0].InputEdges;
			segment.OutputNodes = segments[Predicate.Items.Count - 1].OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			for (int t = 0; t < Predicate.Items.Count; t++)
			{
				segments[t] = BuildSegment(Nodes, Rule, Alphabet, Predicate.Items[t], Edges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments.SelectMany(item => item.InputEdges);
			segment.OutputNodes = segments.SelectMany(item => item.OutputNodes);

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule, Alphabet, Predicate.Item, Edges);
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule, Alphabet, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Alphabet == null) throw new ArgumentNullException("Alphabet");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule, Alphabet, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

	}
}
