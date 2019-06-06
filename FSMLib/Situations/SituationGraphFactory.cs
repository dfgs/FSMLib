using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraphFactory<T> : ISituationGraphFactory<T>
	{
		public SituationGraph<T> BuildSituationGraph(IEnumerable<Rule<T>> Rules)
		{
			SituationNode<T> rootNode;
			SituationGraphSegment<T> segment;
			Sequence<T> predicate;
			SituationGraph<T> graph;

			if (Rules == null) throw new ArgumentNullException("Rules");

			graph = new SituationGraph<T>();

			foreach (Rule<T> rule in Rules)
			{
				predicate = new Sequence<T>();
				predicate.Items.Add(rule.Predicate);
				predicate.Items.Add(ReducePredicate<T>.Instance);

				segment = BuildPredicate(graph,rule, predicate, Enumerable.Empty<SituationEdge<T>>());
				rootNode = CreateNode(graph);
				rootNode.Rule = rule;
				Connect(rootNode.AsEnumerable(), segment.InputEdges);
			}

			return graph;
		}
		private SituationEdge<T> CreateEdgeTo(SituationNode<T> Node, Rule<T> Rule, SituationPredicate<T> Predicate)
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
					//if (edge.NextPredicate is ReducePredicate<T>) continue;
					node.Edges.Add(edge);
				}
			}

		}
		private SituationNode<T> CreateNode(SituationGraph<T> Graph)
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			Graph.Nodes.Add(node);

			return node;
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, BasePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			switch (Predicate)
			{
				case Terminal<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case NonTerminal<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case AnyTerminal<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case EOS<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case ReducePredicate<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case Sequence<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case Or<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case OneOrMore<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case ZeroOrMore<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				case Optional<T> predicate: return BuildPredicate(Graph, Rule, predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, SituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			node = CreateNode(Graph);
			Connect(node.AsEnumerable(), Edges);

			edge = CreateEdgeTo(node, Rule, Predicate);

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edge.AsEnumerable();

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			IEnumerable<SituationEdge<T>> nextEdges;
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			nextEdges = Edges;
			for (int t = Predicate.Items.Count - 1; t >= 0; t--)
			{
				segments[t] = BuildPredicate(Graph, Rule, Predicate.Items[t], nextEdges);
				nextEdges = segments[t].InputEdges;
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments[0].InputEdges;
			segment.OutputNodes = segments[Predicate.Items.Count - 1].OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			for (int t = 0; t < Predicate.Items.Count; t++)
			{
				segments[t] = BuildPredicate(Graph, Rule, Predicate.Items[t], Edges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments.SelectMany(item => item.InputEdges);
			segment.OutputNodes = segments.SelectMany(item => item.OutputNodes);

			return segment;
		}


		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Graph, Rule, Predicate.Item, Edges);
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Graph, Rule, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(SituationGraph<T> Graph, Rule<T> Rule, OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Graph, Rule, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}





	}
}
