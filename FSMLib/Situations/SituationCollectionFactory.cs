using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.ProcessingQueues;
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

		/*private IEnumerable<Rule<T>> DevelopRule(string RuleName)
		{
			ProcessingQueue<string, Rule<T>> queue;

			queue = new ProcessingQueue<string, Rule<T>>();
			queue.Add(RuleName);

			queue.Process((q, ruleName) =>
			{
				foreach (Rule<T> rule in GetRootNodes(ruleName).Select(item => item.Rule))
				{
					q.AddResult(rule);
					foreach (SituationEdge<T> edge in GetRuleInputEdges(rule))
					{
						if (!(edge.Predicate is NonTerminal<T> nonTerminal)) continue;
						q.Add(nonTerminal.Name);
					}
				}
			});

			return queue.Results;
			
		}*/
		private IEnumerable<SituationEdge<T>> DevelopRuleInputEdges(string RuleName)
		{
			ProcessingQueue<string, SituationEdge<T>> queue;

			queue = new ProcessingQueue<string, SituationEdge<T>>();
			queue.Add(RuleName);

			queue.Process((q, ruleName) =>
			{
				foreach (Rule<T> rule in GetRootNodes(ruleName).Select(item => item.Rule))
				{
					foreach (SituationEdge <T> edge in GetRuleInputEdges(rule))
					{
						q.AddResult(edge);
						if (!(edge.Predicate is NonTerminal<T> nonTerminal)) continue;
						q.Add(nonTerminal.Name);
					}
				}
			});

			return queue.Results;
		}
		private IEnumerable<SituationEdge<T>> GetRuleInputEdges(Rule<T> Rule)
		{
			SituationNode<T> node;

			node = graph.Nodes.FirstOrDefault(item => item.Rule == Rule);
			if (node == null) return Enumerable.Empty<SituationEdge<T>>();
			return node.Edges;
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

		private IEnumerable<SituationEdge<T>> GetLoopedEdges(SituationNode<T> Node)
		{
			foreach(SituationEdge<T> edge in Node.Edges)
			{
				if (edge.TargetNode == Node) yield return edge;
			}
		}
		private IEnumerable<IInput<T>> GetTerminalInputsAfterPredicate(NonTerminal<T> NonTerminal)
		{
			SituationEdge<T> edge;
			ProcessingQueue<SituationEdge<T>,BaseTerminalInput<T>> queue;
			IInput<T> input;

			queue = new ProcessingQueue<SituationEdge<T>, BaseTerminalInput<T>>();

			// add edges next to current non terminal
			edge = GetEdge(NonTerminal);
			queue.AddRange(edge.TargetNode.Edges);

			// search for left recursive rules referenced by non terminal
			foreach(SituationEdge<T> developpedEdge in DevelopRuleInputEdges(NonTerminal.Name))
			{
				// left recursive rule case
				if ((developpedEdge.Predicate is NonTerminal<T> recursiveNonTerminal) && (recursiveNonTerminal.Name == NonTerminal.Name))
				{
					queue.AddRange(developpedEdge.TargetNode.Edges);
				}
			}
			
			// search for loops
			foreach(SituationEdge<T> developpedEdge in DevelopRuleInputEdges(NonTerminal.Name))
			{
				queue.AddRange(GetLoopedEdges(developpedEdge.TargetNode));
			}

			queue.Process((q, item) =>
			{
				input = item.Predicate.GetInput();
				if (input is BaseTerminalInput<T> terminalInput) q.AddResult(terminalInput);
				else if (item.Predicate is ReducePredicate<T>) q.AddResult(new EOSInput<T>());
				else if (input is NonTerminalInput<T> nonTerminalInput)
				{
					q.AddRange(DevelopRuleInputEdges(nonTerminalInput.Name));
				}
			});

			return queue.Results;

		}
		/*private bool IsReductionStuationAfterPredicate(NonTerminal<T> NonTerminal)
		{
			SituationEdge<T> edge;


			edge = GetEdge(NonTerminal);
			foreach(SituationEdge<T> nextEdge in  edge.TargetNode.Edges)
			{
				if (nextEdge.Predicate is ReducePredicate<T>) return true;
			}
			return false;
		}*/

		public IEnumerable<Situation<T>> CreateAxiomSituations()
		{
			foreach (SituationNode<T> node in graph.Nodes)
			{
				if ((node.Rule == null) || (!node.Rule.IsAxiom)) continue;
				foreach (SituationEdge<T> edge in node.Edges)
				{
					yield return new Situation<T>() { Rule = edge.Rule, Predicate = edge.Predicate, Input = new EOSInput<T>() };
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
			Situation<T> newSituation;
			IInput<T>[] inputs;
			ProcessingQueue<Situation<T>, Situation<T>> processingQueue;
			//bool isReductionSituationAfterPredicate;

			processingQueue = new ProcessingQueue<Situation<T>, Situation<T>>();
			processingQueue.AddRange(Situations);

			processingQueue.Process((q, situation) =>
			{
				q.AddResult(situation);

				if (!(situation.Predicate is NonTerminal<T> nonTerminal)) return;

				//isReductionSituationAfterPredicate = IsReductionStuationAfterPredicate(nonTerminal);
				inputs = GetTerminalInputsAfterPredicate(nonTerminal).ToArray();


				foreach (SituationEdge<T> developpedEdge in DevelopRuleInputEdges(nonTerminal.Name))
				{
					// basic case
					foreach (BaseTerminalInput<T> input in inputs)
					{
						newSituation = new Situation<T>() { Rule = developpedEdge.Rule, Input = input, Predicate = developpedEdge.Predicate };
						q.AddResult(newSituation);
					}
					// reduction case
					/*if (isReductionSituationAfterPredicate)
					{
						newSituation = new Situation<T>() { Rule = developpedEdge.Rule, Input = new EOSInput<T>(), Predicate = developpedEdge.Predicate };
						q.AddResult(newSituation);
					}*/
					
				}

			});

			developpedSituations = new SituationCollection<T>();
			developpedSituations.AddRange(processingQueue.Results);
			return developpedSituations;
		}
	}
}
