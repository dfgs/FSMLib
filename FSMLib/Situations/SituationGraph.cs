using FSMLib.Inputs;
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

		private SituationNode<T> rootNode;


		public SituationGraph(IEnumerable<Rule<T>> Rules)
		{
			SituationNode<T> rootPredicateNode;
			SituationGraphSegment<T> segment;
			Sequence<T> predicate;
			Dictionary<Rule<T>, SituationGraphSegment<T>> segmentDictionary;

			if (Rules == null) throw new ArgumentNullException("Rules");

			this.inputPredicateNodes = new List<SituationNode<T>>();
			segmentDictionary = new Dictionary<Rule<T>, SituationGraphSegment<T>>();

			foreach(Rule<T> rule in Rules)
			{
				predicate = new Sequence<T>();
				predicate.Items.Add(rule.Predicate);
				predicate.Items.Add(ReducePredicate<T>.Instance);

				segment = BuildPredicate(rule,predicate, Enumerable.Empty<SituationEdge<T>>() );
				rootPredicateNode = CreateNode();
				Connect(rootPredicateNode.AsEnumerable(), segment.InputEdges);
				segmentDictionary.Add(rule,segment);

				if (rule == Rules.First()) rootNode = rootPredicateNode;
			}

			

			foreach(SituationNode<T> node in inputPredicateNodes)
			{
				foreach(NonTerminal<T> nonTerminal in node.Edges.Select(item=>item.Predicate).OfType<NonTerminal<T>>().ToArray())
				{
					foreach(Rule<T> rule in Develop(Rules,segmentDictionary,nonTerminal.Name))
					{
						Connect(node.AsEnumerable(), GetRuleInputEdges(segmentDictionary, rule));
					}
				}
			}


		}

		private IEnumerable<Rule<T>> Develop(IEnumerable<Rule<T>> Rules, Dictionary<Rule<T>, SituationGraphSegment<T>> SegmentDictionary, string Name)
		{
			Stack<Rule<T>> openList;
			List<Rule<T>> closedList;
			Rule<T> current;

			closedList = new List<Rule<T>>();
			openList = new Stack<Rule<T>>();
			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{
				openList.Push(rule);
			}

			while (openList.Count > 0)
			{
				current = openList.Pop();
				closedList.Add(current);
				yield return current;

				foreach (InputPredicate<T> predicate in GetRuleInputEdges(SegmentDictionary,current).Select(item=>item.Predicate))
				{
					if (predicate is NonTerminal<T> nonTerminal)
					{
						foreach (Rule<T> rule in Rules.Where(item => item.Name == nonTerminal.Name))
						{
							if (closedList.Contains(rule)) continue;
							yield return rule;
							openList.Push(rule);
						}
					}
				}
			}
		}

		private IEnumerable<SituationEdge<T>> GetRuleInputEdges(Dictionary<Rule<T>, SituationGraphSegment<T>> SegmentDictionary,Rule<T> Rule)
		{
			SituationGraphSegment<T> segment;

			if (SegmentDictionary.TryGetValue(Rule, out segment)) return segment.InputEdges;
			return Enumerable.Empty<SituationEdge<T>>();

		}
		public IEnumerable<Situation<T>> GetRootSituations()
		{
			if (rootNode == null) yield break;
			foreach(SituationEdge<T> edge in rootNode.Edges)
			{
				yield return new Situation<T>() { Rule = edge.Rule, Predicate = edge.Predicate };
			}
		}


		public IEnumerable<Situation<T>> GetNextSituations(Situation<T> CurrentSituation)
		{
			SituationEdge<T> edge;

			edge = inputPredicateNodes.SelectMany(item=>item.Edges).FirstOrDefault(item => (item.Predicate ==CurrentSituation.Predicate) && (item.Rule==CurrentSituation.Rule) );
			if (edge == null) return Enumerable.Empty<Situation<T>>();

			return edge.TargetNode.Edges.Select(item=> new Situation<T>() { Rule=item.Rule,Predicate=item.Predicate }  );
		}

		public IEnumerable<BaseInput<T>> GetInputsAfterPredicate(BasePredicate<T> CurrentPredicate)
		{
			SituationEdge<T> edge;
			List<BaseInput<T>> items;

			items = new List<BaseInput<T>>();

			edge = inputPredicateNodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == CurrentPredicate);
			if (edge != null)
			{
				foreach (SituationEdge<T> nextEdge in edge.TargetNode.Edges)
				{
					items.Add(nextEdge.Predicate.GetInput());
				}
			}

			return items.DistinctEx(); ;
		}

		private SituationNode<T> GetSituationNode(Situation<T> Situation)
		{
			SituationNode<T> node;

			node = inputPredicateNodes.FirstOrDefault(n => n.Edges.FirstOrDefault(e => (e.Predicate == Situation.Predicate) && (e.Rule==Situation.Rule) ) != null);

			return node;
		}

		/*public ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations)
		{
			SituationCollection<T> developpedSituations;
			SituationNode<T> node;
			Situation<T> newSituation;

			developpedSituations = new SituationCollection<T>();

			foreach(Situation<T> situation in Situations)
			{
				node = GetSituationNode(situation);
				foreach(SituationEdge<T> edge in node.Edges)
				{
					newSituation = new Situation<T>() { Rule = edge.Rule, Input = situation.Input, Predicate = edge.Predicate };
					developpedSituations.Add(newSituation);
				}
			}

			return developpedSituations;
		}*/


		


		public bool Contains(BasePredicate<T> Predicate)
		{
			SituationEdge<T> egde;

			egde = inputPredicateNodes.SelectMany(item=>item.Edges).FirstOrDefault(item => item.Predicate == Predicate);
			return (egde != null);
		}
		








		private SituationNode<T> CreateNode()
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			inputPredicateNodes.Add(node);

			return node;
		}
		private SituationEdge<T> CreateEdgeTo(SituationNode<T> Node,Rule<T> Rule, InputPredicate<T> Predicate)
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
		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, BasePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			switch (Predicate)
			{
				case Terminal<T> predicate: return BuildPredicate(Rule, predicate,Edges);
				case NonTerminal<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case AnyTerminal<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case EOS<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case ReducePredicate<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case Sequence<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case Or<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case OneOrMore<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case ZeroOrMore<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				case Optional<T> predicate: return BuildPredicate(Rule, predicate, Edges);
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}
		
		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule,InputPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationNode<T> node;
			SituationEdge<T> edge;
			SituationGraphSegment<T> segment;

			node = CreateNode();
			Connect(node.AsEnumerable(), Edges);

			edge = CreateEdgeTo(node,Rule, Predicate);

			segment = new SituationGraphSegment<T>();
			segment.OutputNodes = node.AsEnumerable();
			segment.InputEdges = edge.AsEnumerable();

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			IEnumerable<SituationEdge<T>>  nextEdges;
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			nextEdges = Edges;
			for (int t = Predicate.Items.Count - 1; t >= 0; t--)
			{
				segments[t] = BuildPredicate(Rule,Predicate.Items[t], nextEdges);
				nextEdges = segments[t].InputEdges;
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments[0].InputEdges;
			segment.OutputNodes = segments[Predicate.Items.Count - 1].OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T>[] segments;
			SituationGraphSegment<T> segment;

			segments = new SituationGraphSegment<T>[Predicate.Items.Count];
			for (int t = 0; t <Predicate.Items.Count; t++)
			{
				segments[t] = BuildPredicate(Rule, Predicate.Items[t], Edges);
			}

			segment = new SituationGraphSegment<T>();
			segment.InputEdges = segments.SelectMany(item=>item.InputEdges);
			segment.OutputNodes = segments.SelectMany(item => item.OutputNodes);

			return segment;
		}


		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment,segment;

			itemSegment = BuildPredicate(Rule, Predicate.Item, Edges);
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment,segment;

			itemSegment = BuildPredicate(Rule, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes,itemSegment.InputEdges);

			
			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges.Concat(Edges);
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}

		private SituationGraphSegment<T> BuildPredicate(Rule<T> Rule, OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges)
		{
			SituationGraphSegment<T> itemSegment, segment;

			itemSegment = BuildPredicate(Rule, Predicate.Item, Edges);
			Connect(itemSegment.OutputNodes, itemSegment.InputEdges);


			segment = new SituationGraphSegment<T>();
			segment.InputEdges = itemSegment.InputEdges;
			segment.OutputNodes = itemSegment.OutputNodes;

			return segment;
		}


	}
}
