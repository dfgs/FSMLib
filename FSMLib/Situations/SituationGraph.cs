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
		public List<SituationNode<T>> Nodes
		{
			get;
			set;
		}

		//private Dictionary<Rule<T>, SituationNode<T>> ruleNodes;


		public SituationGraph()
		{
			Nodes = new List<SituationNode<T>>();
		}

		private IEnumerable<Rule<T>> GetDeveloppedRules(string Name)
		{
			Stack<Rule<T>> openList;
			List<Rule<T>> closedList;
			Rule<T> current;

			closedList = new List<Rule<T>>();
			openList = new Stack<Rule<T>>();

			foreach (Rule<T> rule in Nodes.Select(item=>item.Rule).Where(item => (item!=null) && (item.Name == Name)))
			{
				openList.Push(rule);
			}

			while (openList.Count > 0)
			{
				current = openList.Pop();
				closedList.Add(current);
				yield return current;

				foreach (SituationPredicate<T> predicate in GetRuleInputEdges(current).Select(item=>item.Predicate))
				{
					if (predicate is NonTerminal<T> nonTerminal)
					{
						foreach (Rule<T> rule in Nodes.Select(item => item.Rule).Where(item => (item != null) && (item.Name == nonTerminal.Name)))
						{
							if (closedList.Contains(rule)) continue;
							yield return rule;
							openList.Push(rule);
						}
					}
				}
			}
		}

		private IEnumerable<SituationEdge<T>> GetRuleInputEdges(Rule<T> Rule)
		{
			SituationNode<T> node;

			node = Nodes.FirstOrDefault(item => item.Rule == Rule);
			if (node!=null) return node.Edges;
			return Enumerable.Empty<SituationEdge<T>>();

		}
		



		public IEnumerable<Situation<T>> CreateNextSituations(IEnumerable<Situation<T>> CurrentSituations, IInput<T> Input)
		{
			IEnumerable<Situation<T>> matchingSituations,nextSituations;
			SituationEdge<T> edge;


			matchingSituations = CurrentSituations.Where(s => s.Predicate.GetInputs().FirstOrDefault(i => i.Match(Input)) != null);


			foreach (Situation<T> situation in matchingSituations)
			{
				edge = Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => (item.Predicate == situation.Predicate) && (item.Rule == situation.Rule));
				if (edge == null) continue;

				nextSituations= edge.TargetNode.Edges.Select(item => new Situation<T>() { Predicate = item.Predicate, Rule = item.Rule, Input = situation.Input });

				foreach (Situation<T> nextSituation in nextSituations)
				{
					yield return nextSituation;
				}
			}

		}


		private IEnumerable<IInput<T>> GetTerminalInputsAfterPredicate(NonTerminal<T> NonTerminal, BaseTerminalInput<T> Input)
		{
			SituationEdge<T> edge;
			List<IInput<T>> items;
			//BaseInput<T> input;
			Stack<SituationEdge<T>> openList;
			IEnumerable<SituationEdge<T>> recursiveEdges;

			items = new List<IInput<T>>();

			edge = Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == NonTerminal);
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
				foreach(SituationEdge<T> recursiveEdge in recursiveEdges)
				{
					foreach (SituationEdge<T> nextEdge in recursiveEdge.TargetNode.Edges)
					{
						openList.Push(nextEdge);
					}
				}
			}

			// process stack
			while (openList.Count>0)
			{
				edge = openList.Pop();
				if (edge.Predicate is ReducePredicate<T>) items.Add(new EOSInput<T>());

				foreach (BaseInput<T> input in edge.Predicate.GetInputs())
				{
					/*if (input is ReduceInput<T>) items.Add(Input);
					else*/
					
					if (input is BaseTerminalInput<T> terminalInput) items.Add(terminalInput);
					else if (input is NonTerminalInput<T> nonTerminalInput)
					{
						foreach (Rule<T> rule in GetDeveloppedRules(nonTerminalInput.Name))
						{
							foreach (SituationEdge<T> developpedEdge in GetRuleInputEdges(rule))
							{
								openList.Push(developpedEdge);
							}
						}
					}
				}
			}

			return items.DistinctEx(); ;
		}

		private SituationNode<T> GetSituationNode(Situation<T> Situation)
		{
			SituationNode<T> node;

			node = Nodes.FirstOrDefault(n => n.Edges.FirstOrDefault(e => (e.Predicate == Situation.Predicate) && (e.Rule==Situation.Rule) ) != null);

			return node;
		}

		public ISituationCollection<T> Develop(IEnumerable<Situation<T>> Situations)
		{
			SituationCollection<T> developpedSituations;
			SituationNode<T> node;
			Situation<T> newSituation;
			IInput<T>[] inputs;

			developpedSituations = new SituationCollection<T>();

			foreach(Situation<T> situation in Situations)
			{
				node = GetSituationNode(situation);
				foreach(SituationEdge<T> edge in node.Edges)
				{
					newSituation = new Situation<T>() { Rule = edge.Rule, Input = situation.Input, Predicate = edge.Predicate };
					developpedSituations.Add(newSituation);
					if (edge.Predicate is NonTerminal<T> nonTerminal)
					{
						inputs = GetTerminalInputsAfterPredicate(nonTerminal,situation.Input).ToArray();

						foreach(Rule<T> developpedRule in GetDeveloppedRules(nonTerminal.Name))
						{

							foreach (SituationEdge<T> developpedEdge in GetRuleInputEdges(developpedRule))
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
			}

			return developpedSituations;
		}


		


		public bool Contains(SituationPredicate<T> Predicate)
		{
			SituationEdge<T> egde;

			egde = Nodes.SelectMany(item=>item.Edges).FirstOrDefault(item => item.Predicate == Predicate);
			return (egde != null);
		}
		








		
		
	


	}
}
