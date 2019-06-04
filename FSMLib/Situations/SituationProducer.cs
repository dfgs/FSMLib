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
	

		
		
		public IEnumerable<NonTerminalInput<T>> GetNextNonTerminalInputs(IEnumerable<Situation<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<NonTerminalInput<T>>().DistinctEx();
		}
		public IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<BaseTerminalInput<T>>().Where(item=>!(item is ReduceInput<T> )).DistinctEx();
		}
		//*/


		public ISituationCollection<T> GetNextSituations(ISituationGraph<T> SituationGraph, IEnumerable<Situation<T>> Situations,IInput<T> Input)
		{
			List<Situation<T>> results;
			SituationCollection<T> result;

			if (SituationGraph == null) throw new ArgumentNullException("SituationGraph");
			if (Situations == null) throw new ArgumentNullException("Situations");
			if (Input == null) throw new ArgumentNullException("Input");

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				if (!situation.Predicate.GetInput().Match(Input)) continue;
				foreach (Situation<T> nextSituation in SituationGraph.GetNextSituations(situation))
				{
					results.Add(nextSituation);
				}
				
			}

			result = new SituationCollection<T>();
			result.AddRange(results);
			return result;
		}
		public void Connect(IEnumerable<State<T>> States, IEnumerable<Shift<T>> Actions)
		{

			if (States == null) throw new ArgumentNullException("States");
			if (Actions == null) throw new ArgumentNullException("Actions");

			foreach (State<T> state in States)
			{
				foreach (Shift<T> action in Actions)
				{
					switch (action)
					{
						case ShiftOnTerminal<T> tr:
							if (state.TerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.TerminalActions.Add(tr);
							break;
						case ShiftOnNonTerminal<T> tr:
							if (state.NonTerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.NonTerminalActions.Add(tr);
							break;
						default:
							throw (new NotImplementedException("Invalid action type"));
					}
				}
			}
		}


		



		/*public ISituationCollection<T> Develop(ISituationGraph<T> SituationGraph,IEnumerable<Situation<T>> Situations, IEnumerable<Rule<T>> Rules)
		{
			SituationCollection<T> results;
			int index;
			Situation<T> developpedSituation;

			results = new SituationCollection<T>();
			results.AddRange(Situations);
			index = 0;
			while(index<results.Count)
			{
				if ((results[index].Predicate is NonTerminal<T> nonTerminal))
				{
					foreach (InputPredicate<T> developpedPredicate in SituationGraph.Develop(Rules, nonTerminal.Name))
					{

					}

					foreach (Rule<T> rule in Rules.Where(item => item.Name == nonTerminal.Name))
					{
						foreach (InputPredicate<T> developpedPredicate in SituationGraph.GetRuleInputPredicates(rule.Name))
						{
							foreach(BaseTerminalInput<T> nextInput in SituationGraph.GetInputsAfterPredicate(nonTerminal))
							{
								developpedSituation = new Situation<T>() { Rule = rule, Predicate = developpedPredicate, Input = nextInput};
								results.Add(developpedSituation);
							}
						}
					}
				}
				index++;

			}

			return results;
		}*/
		


	}
}
