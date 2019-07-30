using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Predicates;
using FSMLib.ProcessingQueues;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Situations
{
	public class SituationCollectionFactory: ISituationCollectionFactory<char>
	{
		private SituationGraph<char> graph;

		public SituationCollectionFactory(SituationGraph<char> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
		}

		private IEnumerable<SituationNode<char>> GetRootNodes(string RuleName)
		{
			foreach (SituationNode<char> node in graph.Nodes)
			{
				if ((node.Rule != null) && (node.Rule.Name == RuleName)) yield return node;
			}

		}

		
		private IEnumerable<SituationEdge<char>> DevelopRuleInputEdges(string RuleName)
		{
			ProcessingQueue<string, SituationEdge<char>> queue;

			queue = new ProcessingQueue<string, SituationEdge<char>>();
			queue.Add(RuleName);

			queue.Process((q, ruleName) =>
			{
				foreach (IRule<char> rule in GetRootNodes(ruleName).Select(item => item.Rule))
				{
					foreach (SituationEdge <char> edge in GetRuleInputEdges(rule))
					{
						q.AddResult(edge);
						if (!(edge.Predicate is INonTerminalPredicate<char> nonTerminal)) continue;
						q.Add(nonTerminal.Name);
					}
				}
			});

			return queue.Results;
		}
		private IEnumerable<SituationEdge<char>> GetRuleInputEdges(IRule<char> Rule)
		{
			SituationNode<char> node;

			node = graph.Nodes.FirstOrDefault(item => item.Rule == Rule);
			if (node == null) return Enumerable.Empty<SituationEdge<char>>();
			return node.Edges;
		}

		private SituationEdge<char> GetEdge(ISituationPredicate<char> Predicate)
		{
			return graph.Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == Predicate);
		}

		private SituationNode<char> GetSituationNode(Situation<char> Situation)
		{
			SituationNode<char> node;

			node = graph.Nodes.FirstOrDefault(n => n.Edges.FirstOrDefault(e => (e.Predicate == Situation.Predicate) && (e.Rule == Situation.Rule)) != null);

			return node;
		}

		private IEnumerable<SituationEdge<char>> GetLoopedEdges(SituationNode<char> Node)
		{
			foreach(SituationEdge<char> edge in Node.Edges)
			{
				if (edge.TargetNode == Node) yield return edge;
			}
		}
		private IEnumerable<IInput<char>> GetTerminalInputsAfterPredicate(INonTerminalPredicate<char> NonTerminal)
		{
			SituationEdge<char> edge;
			ProcessingQueue<SituationEdge<char>,ITerminalInput<char>> queue;
			IInput<char> input;

			queue = new ProcessingQueue<SituationEdge<char>, ITerminalInput<char>>();

			// add edges next to current non terminal
			edge = GetEdge(NonTerminal);
			queue.AddRange(edge.TargetNode.Edges);

			// search for left recursive rules referenced by non terminal
			foreach(SituationEdge<char> developpedEdge in DevelopRuleInputEdges(NonTerminal.Name))
			{
				// left recursive rule case
				if ((developpedEdge.Predicate is INonTerminalPredicate<char> recursiveNonTerminal) && (recursiveNonTerminal.Name == NonTerminal.Name))
				{
					queue.AddRange(developpedEdge.TargetNode.Edges);
				}
			}
			
			// search for loops
			foreach(SituationEdge<char> developpedEdge in DevelopRuleInputEdges(NonTerminal.Name))
			{
				queue.AddRange(GetLoopedEdges(developpedEdge.TargetNode));
			}

			queue.Process((q, item) =>
			{
				input = item.Predicate.GetInput();
				if (input is LetterInput terminalInput) q.AddResult(terminalInput);
				else if (item.Predicate is IReducePredicate<char>) q.AddResult(new EOSInput());
				else if (input is NonTerminalInput nonTerminalInput)
				{
					q.AddRange(DevelopRuleInputEdges(nonTerminalInput.Name));
				}
			});

			return queue.Results;

		}
		

		public IEnumerable<Situation<char>> CreateAxiomSituations()
		{
			foreach (SituationNode<char> node in graph.Nodes)
			{
				if ((node.Rule == null) || (!node.Rule.IsAxiom)) continue;
				foreach (SituationEdge<char> edge in node.Edges)
				{
					yield return new Situation<char>() { Rule = edge.Rule, Predicate = edge.Predicate, Input = new EOSInput() };
				}

			}
		}

		public IEnumerable<Situation<char>> CreateNextSituations(IEnumerable<Situation<char>> CurrentSituations, IActionInput<char> Input)
		{
			IEnumerable<Situation<char>> matchingSituations;
			SituationEdge<char> edge;
			Situation<char> nextSituation;
		

			matchingSituations = CurrentSituations.Where(s => s.Predicate.Match(Input));

			foreach (Situation<char> situation in matchingSituations)
			{
				edge = GetEdge(situation.Predicate);

				foreach (SituationEdge<char> nextEdge in edge.TargetNode.Edges)
				{
					nextSituation = new Situation<char>() { Predicate = nextEdge.Predicate, Rule = nextEdge.Rule, Input = situation.Input };
					yield return nextSituation;
				}
			}

		}

		public ISituationCollection<char> Develop(IEnumerable<Situation<char>> Situations)
		{
			SituationCollection<char> developpedSituations;
			Situation<char> newSituation;
			IInput<char>[] inputs;
			ProcessingQueue<Situation<char>, Situation<char>> processingQueue;
			//bool isReductionSituationAfterPredicate;

			processingQueue = new ProcessingQueue<Situation<char>, Situation<char>>();
			processingQueue.AddRange(Situations);

			processingQueue.Process((q, situation) =>
			{
				q.AddResult(situation);

				if (!(situation.Predicate is INonTerminalPredicate<char> nonTerminal)) return;

				//isReductionSituationAfterPredicate = IsReductionStuationAfterPredicate(nonTerminal);
				inputs = GetTerminalInputsAfterPredicate(nonTerminal).ToArray();


				foreach (SituationEdge<char> developpedEdge in DevelopRuleInputEdges(nonTerminal.Name))
				{
					// basic case
					foreach (ITerminalInput<char> input in inputs)
					{
						newSituation = new Situation<char>() { Rule = developpedEdge.Rule, Input = input, Predicate = developpedEdge.Predicate };
						q.AddResult(newSituation);
					}
					
					
				}

			});

			developpedSituations = new SituationCollection<char>();
			developpedSituations.AddRange(processingQueue.Results);
			return developpedSituations;
		}


		



	}
}
