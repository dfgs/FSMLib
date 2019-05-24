using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Table;
using FSMLib.Actions;

namespace FSMLib.Rules
{
	public class SituationProducer<T> : ISituationProducer<T>
	{
		/*public IEnumerable<Action<T>> GetAllActions(IEnumerable<Situation<T>> Situations)
		{
			return ;
		}*/

		

		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].NonTerminalActions).Select(item => item.Name).Distinct();
		}
		public IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].TerminalActions).Select(item => item.Input).Distinct();
		}

	

		public IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, BaseTerminalInput<T> Input)
		{
			Situation<T> newSituation;
			List<Situation<T>> results;

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				foreach (ShiftOnTerminal<T> action in situation.AutomatonTable.States[situation.StateIndex].TerminalActions)
				{
					if (!action.Input.Match(Input)) continue;
					newSituation = new Situation<T>() { AutomatonTable = situation.AutomatonTable, StateIndex = action.TargetStateIndex };
					results.Add(newSituation);
				}
			}

			return results.DistinctEx();
		}

		public IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, string Name)
		{
			Situation<T> newSituation;
			List<Situation<T>> results;

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				foreach (ShiftOnNonTerminal<T> action in situation.AutomatonTable.States[situation.StateIndex].NonTerminalActions)
				{
					if (action.Name!=Name) continue;
					newSituation = new Situation<T>() { AutomatonTable = situation.AutomatonTable, StateIndex = action.TargetStateIndex };
					results.Add(newSituation);
				}
			}

			return results.Distinct();
		}

		
	}
}
