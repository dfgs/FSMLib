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
		private SituationEdge<T> CreateEdgeTo(SituationNode<T> Node, IRule<T> Rule, ISituationPredicate<T> Predicate)
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


		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");


			switch (Predicate)
			{
				case IEOSPredicate<T> predicate: return BuildSegment(Nodes, Rule, predicate, Edges);
				case IReducePredicate<T> predicate: return BuildSegment(Nodes, Rule, predicate, Edges);
				case INonTerminalPredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case IAnyTerminalPredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case ITerminalRangePredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case ISituationPredicate<T> predicate: return BuildSegment(Nodes, Rule, predicate, Edges);
				case ISequencePredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case IOrPredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case IOneOrMorePredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case IZeroOrMorePredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				case IOptionalPredicate<T> predicate: return BuildSegment(Nodes, Rule,  predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  ISituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
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
		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  ISequencePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			IEnumerable<SituationEdge<T>> nextEdges;
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;
			IPredicate<T>[] items;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			items = Predicate.Items.ToArray();
			segments = new SituationGraphSegment<T>[items.Length];
			nextEdges = Edges;
			for (int t = items.Length - 1; t >= 0; t--)
			{
				segments[t] = BuildSegment(Nodes, Rule,  items[t], nextEdges);
				nextEdges = segments[t].InputEdges;
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments[0].InputEdges;
			segment.OutputNodes = segments[items.Length - 1].OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IOrPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;
			IPredicate<T>[] items;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			items = Predicate.Items.ToArray();
			segments = new SituationGraphSegment<T>[items.Length];
			for (int t = 0; t < items.Length; t++)
			{
				segments[t] = BuildSegment(Nodes, Rule,  items[t], Edges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments.SelectMany(item => item.InputEdges);
			segment.OutputNodes = segments.SelectMany(item => item.OutputNodes);

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IOptionalPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule,  Predicate.Item, Edges);
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IZeroOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule,  Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		public SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IOneOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Rule == null) throw new ArgumentNullException("Rule");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (Edges == null) throw new ArgumentNullException("Edges");

			itemSegment = BuildSegment(Nodes, Rule,  Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

	}
}
