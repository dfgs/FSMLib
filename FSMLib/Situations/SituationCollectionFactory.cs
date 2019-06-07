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
	public class SituationCollectionFactory<T>: ISituationCollectionFactory<T>
	{
		private SituationGraph<T> graph;

		public SituationCollectionFactory(SituationGraph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
		}

		private IEnumerable<SituationNode<T>> GetRootNodes(string RuleName)
		{
			foreach (SituationNode<T> node in graph.Nodes)
			{
				if ((node.Rule != null) && (node.Rule.Name == RuleName)) yield return node;
			}

		}

		private IEnumerable<Rule<T>> GetDeveloppedRules(string RuleName)
		{
			Stack<Rule<T>> openList;
			List<Rule<T>> closedList;
			Rule<T> current;

			closedList = new List<Rule<T>>();
			openList = new Stack<Rule<T>>();

			foreach (SituationNode<T> rootNode in GetRootNodes(RuleName))
			{
				openList.Push(rootNode.Rule);
			}

			while (openList.Count > 0)
			{
				current = openList.Pop();
				closedList.Add(current);
				yield return current;

				foreach (SituationPredicate<T> predicate in GetRuleInputEdges(current).Select(item => item.Predicate))
				{
					if (!(predicate is NonTerminal<T> nonTerminal)) continue;

					foreach (Rule<T> rule in GetRootNodes(nonTerminal.Name).Select(item => item.Rule))
					{
						if (!closedList.Contains(rule)) openList.Push(rule);
					}
				}
			}
		}

		private IEnumerable<SituationEdge<T>> GetRuleInputEdges(Rule<T> Rule)
		{
			SituationNode<T> node;

			node = graph.Nodes.FirstOrDefault(item => item.Rule == Rule);
			if (node == null) return Enumerable.Empty<SituationEdge<T>>();
			return node.Edges;
		}

		private IEnumerable<SituationEdge<T>> GetDeveloppedRuleInputEdges(string RuleName)
		{
			foreach (Rule<T> rule in GetDeveloppedRules(RuleName))
			{
				foreach (SituationEdge<T> developpedEdge in GetRuleInputEdges(rule))
				{
					yield return developpedEdge;
				}
			}
		}

		private SituationEdge<T> GetEdge(ISituationPredicate<T> Predicate)
		{
			return graph.Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == Predicate);
		}

		private SituationNode<T> GetSituationNode(Situation<T> Situation)
		{
			SituationNode<T> node;

			node = graph.Nodes.FirstOrDefault(n => n.Edges.FirstOrDefault(e => (e.Predicate == Situation.Predicate) && (e.Rule == Situation.Rule)) != null);

			return node;
		}

		private IEnumerable<IInput<T>> GetTerminalInputsAfterPredicate(NonTerminal<T> NonTerminal, BaseTerminalInput<T> Input)
		{
			SituationEdge<T> edge;
			List<IInput<T>> items;
			//BaseInput<T> input;
			Stack<SituationEdge<T>> openList;
			IEnumerable<SituationEdge<T>> recursiveEdges;

			items = new List<IInput<T>>();

			edge = GetEdge(NonTerminal);// Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == NonTerminal);
			if (edge == null) return items;

			openList = new Stack<SituationEdge<T>>();

			// add edges after non terminal
			foreach (SituationEdge<T> nextEdge in edge.TargetNode.Edges)
			{
				openList.Push(nextEdge);
			}

			// check left recursive rules
			foreach (Rule<T> rule in GetDeveloppedRules(NonTerminal.Name))
			{
				recursiveEdges = GetRuleInputEdges(rule).Where(item => (item.Predicate is NonTerminal<T> nonTerminal) && (nonTerminal.Name == rule.Name));
				foreach (SituationEdge<T> recursiveEdge in recursiveEdges)
				{
					foreach (SituationEdge<T> nextEdge in recursiveEdge.TargetNode.Edges)
					{
						openList.Push(nextEdge);
					}
				}
			}

			// process stack
			while (openList.Count > 0)
			{
				edge = openList.Pop();
				if (edge.Predicate is ReducePredicate<T>) items.Add(new EOSInput<T>());

				foreach (BaseInput<T> input in edge.Predicate.GetInputs())
				{

					if (input is BaseTerminalInput<T> terminalInput)
					{
						items.Add(terminalInput);
						continue;
					}

					if (input is NonTerminalInput<T> nonTerminalInput)
					{
						foreach (SituationEdge<T> developpedEdge in GetDeveloppedRuleInputEdges(nonTerminalInput.Name))
						{
							openList.Push(developpedEdge);
						}

					}

				}
			}

			return items.DistinctEx(); ;
		}

		public IEnumerable<Situation<T>> CreateAxiomSituations()
		{
			foreach (SituationNode<T> node in graph.Nodes)
			{
				if ((node.Rule == null) || (!node.Rule.IsAxiom)) continue;
				foreach (SituationEdge<T> edge in node.Edges)
				{
					yield return new Situation<T>() { Rule = edge.Rule, Predicate = edge.Predicate };
				}

			}
		}

		public IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IInput<T> Input)
		{
			IEnumerable<Situation<T>> matchingSituations;
			SituationEdge<T> edge;
			Situation<T> nextSituation;


			matchingSituations = CurrentSituations.Where(s => s.Predicate.Match(Input));

			foreach (Situation<T> situation in matchingSituations)
			{
				edge = GetEdge(situation.Predicate);
				if (edge == null) continue;

				foreach (SituationEdge<T> nextEdge in edge.TargetNode.Edges)
				{
					nextSituation = new Situation<T>() { Predicate = nextEdge.Predicate, Rule = nextEdge.Rule, Input = situation.Input };
					yield return nextSituation;
				}
			}

		}

	

		public ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations)
		{
			SituationCollection<T> developpedSituations;
			SituationNode<T> node;
			Situation<T> newSituation;
			IInput<T>[] inputs;

			developpedSituations = new SituationCollection<T>();

			foreach (Situation<T> situation in Situations)
			{
				node = GetSituationNode(situation);
				foreach (SituationEdge<T> edge in node.Edges)
				{
					newSituation = new Situation<T>() { Rule = edge.Rule, Input = situation.Input, Predicate = edge.Predicate };
					developpedSituations.Add(newSituation);
					if (edge.Predicate is NonTerminal<T> nonTerminal)
					{
						inputs = GetTerminalInputsAfterPredicate(nonTerminal, situation.Input).ToArray();

						foreach (SituationEdge<T> developpedEdge in GetDeveloppedRuleInputEdges(nonTerminal.Name))
						{
							foreach (BaseTerminalInput<T> input in inputs)
							{
								newSituation = new Situation<T>() { Rule = developpedEdge.Rule, Input = input, Predicate = developpedEdge.Predicate };
								developpedSituations.Add(newSituation);
							}

						}

					}

				}
			}

			return developpedSituations;
		}
	}
}
