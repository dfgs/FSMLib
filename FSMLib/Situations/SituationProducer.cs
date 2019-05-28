using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;

namespace FSMLib.Situations
{
	public class SituationProducer<T> : ISituationProducer<T>
	{
	

		

		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation2<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<NonTerminalInput<T>>().Select(item=>item.Name).DistinctEx();
		}
		public IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation2<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<TerminalInput<T>>().DistinctEx();
		}



		public IEnumerable<Situation2<T>> GetNextSituations(ISituationGraph<T> SituationGraph, IEnumerable<Situation2<T>> Situations,IInput<T> Input)
		{
			Situation2<T> newSituation;
			List<Situation2<T>> results;

			if (SituationGraph == null) throw new ArgumentNullException("SituationGraph");
			if (Situations == null) throw new ArgumentNullException("Situations");
			if (Input == null) throw new ArgumentNullException("Input");

			results = new List<Situation2<T>>();
			foreach (Situation2<T> situation in Situations)
			{
				if (!situation.Predicate.GetInput().Match(Input)) continue;
				foreach (InputPredicate<T> nextPredicate in SituationGraph.GetNextPredicates(situation.Predicate))
				{
					newSituation = new Situation2<T>() { Rule=situation.Rule,Predicate=nextPredicate,ParentPredicate=situation.ParentPredicate };
					results.Add(newSituation);
				}
				
			}

			return results.DistinctEx();
			
				
		}
		public void Connect(IEnumerable<State<T>> States, IEnumerable<BaseAction<T>> Actions)
		{

			if (States == null) throw new ArgumentNullException("States");
			if (Actions == null) throw new ArgumentNullException("Actions");

			foreach (State<T> state in States)
			{
				foreach (BaseAction<T> action in Actions)
				{
					switch (action)
					{
						case ShiftOnTerminal<T> tr:
							if (state.TerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.TerminalActions.Add(tr);
							break;
						case ShiftOnNonTerminal<T> tr:
							if (state.NonTerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.NonTerminalActions.Add(tr);
							break;
						case Reduce<T> tr:
							if (state.ReductionActions.FirstOrDefault(item => item.Equals(tr)) == null) state.ReductionActions.Add(tr);
							break;
						/*case Accept<T> tr:
							if (state.AcceptActions.FirstOrDefault(item => item.Equals(tr)) == null) state.AcceptActions.Add(tr);
							break;*/

						default:
							throw (new NotImplementedException("Invalid action type"));
					}

				}
			}
		}

		public IEnumerable<Situation2<T>> Develop(ISituationGraph<T> SituationGraph,IEnumerable<Situation2<T>> Situations, IEnumerable<Rule<T>> Rules)
		{
			List<Situation2<T>> results;
			int index;
			Situation2<T> developpedSituation;

			results = new List<Situation2<T>>();
			results.AddRange(Situations);
			index = 0;
			while(index<results.Count)
			{
				if ((results[index].Predicate is NonTerminal<T> nonTerminal))
				{
					foreach (Rule<T> rule in Rules.Where(item => item.Name == nonTerminal.Name))
					{
						foreach (InputPredicate<T> developpedPredicate in SituationGraph.GetRootInputPredicates(rule.Predicate))
						{
							developpedSituation = new Situation2<T>() { Rule = rule, Predicate = developpedPredicate,ParentPredicate=nonTerminal };
							if (results.FirstOrDefault(item => item.Equals(developpedSituation)) == null) results.Add(developpedSituation);
						}
					}
				}
				index++;

			}

			return results;
		}
		


	}
}
