using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.ProcessingQueues;
using FSMLib.Rules;
using FSMLib.Situations;
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

		
		private IEnumerable<SituationEdge<T>> DevelopRuleInputEdges(string RuleName)
		{
			ProcessingQueue<string, SituationEdge<T>> queue;

			queue = new ProcessingQueue<string, SituationEdge<T>>();
			queue.Add(RuleName);

			queue.Process((q, ruleName) =>
			{
				foreach (IRule<T> rule in GetRootNodes(ruleName).Select(item => item.Rule))
				{
					foreach (SituationEdge <T> edge in GetRuleInputEdges(rule))
					{
						q.AddResult(edge);
						if (!(edge.Predicate is INonTerminalPredicate<T> nonTerminal)) continue;
						q.Add(nonTerminal.Name);
					}
				}
			});

			return queue.Results;
		}
		private IEnumerable<SituationEdge<T>> GetRuleInputEdges(IRule<T> Rule)
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
		private IEnumerable<IReduceInput<T>> GetTerminalInputsAfterPredicate(INonTerminalPredicate<T> NonTerminal)
		{
			SituationEdge<T> edge;
			ProcessingQueue<SituationEdge<T>, IReduceInput<T>> queue;
			//IInput<T> input;

			queue = new ProcessingQueue<SituationEdge<T>, IReduceInput<T>>();

			// add edges next to current non terminal
			edge = GetEdge(NonTerminal);
			queue.AddRange(edge.TargetNode.Edges);

			// search for left recursive rules referenced by non terminal
			foreach(SituationEdge<T> developpedEdge in DevelopRuleInputEdges(NonTerminal.Name))
			{
				// left recursive rule case
				if ((developpedEdge.Predicate is INonTerminalPredicate<T> recursiveNonTerminal) && (recursiveNonTerminal.Name == NonTerminal.Name))
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
				foreach (IInput<T> input in item.Predicate.GetInputs())
				{
					if (input is IReduceInput<T> terminalInput) q.AddResult(terminalInput);
					//else if (item.Predicate is IReducePredicate<T>) q.AddResult(new EOSInput<T>());
					else if (input is INonTerminalInput<T> nonTerminalInput)
					{
						q.AddRange(DevelopRuleInputEdges(nonTerminalInput.Name));
					}
				}
			});

			return queue.Results;

		}
		

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

		public IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IActionInput<T> Input)
		{
			IEnumerable<Situation<T>> matchingSituations;
			SituationEdge<T> edge;
			Situation<T> nextSituation;
		

			matchingSituations = CurrentSituations.Where(s => s.Predicate.GetInputs().Where(i=>i.Match(Input)).Any() );

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
			IReduceInput<T>[] inputs;
			ProcessingQueue<Situation<T>, Situation<T>> processingQueue;
			//bool isReductionSituationAfterPredicate;

			processingQueue = new ProcessingQueue<Situation<T>, Situation<T>>();
			processingQueue.AddRange(Situations);

			processingQueue.Process((q, situation) =>
			{
				q.AddResult(situation);

				if (!(situation.Predicate is INonTerminalPredicate<T> nonTerminal)) return;

				//isReductionSituationAfterPredicate = IsReductionStuationAfterPredicate(nonTerminal);
				inputs = GetTerminalInputsAfterPredicate(nonTerminal).ToArray();


				foreach (SituationEdge<T> developpedEdge in DevelopRuleInputEdges(nonTerminal.Name))
				{
					// basic case
					foreach (IReduceInput<T> input in inputs)
					{
						newSituation = new Situation<T>() { Rule = developpedEdge.Rule, Input = input, Predicate = developpedEdge.Predicate };
						q.AddResult(newSituation);
					}
					
					
				}

			});

			developpedSituations = new SituationCollection<T>();
			developpedSituations.AddRange(processingQueue.Results);
			return developpedSituations;
		}


		



	}
}
