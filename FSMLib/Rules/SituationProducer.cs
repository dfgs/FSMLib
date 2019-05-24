using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;

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
			return Situations.SelectMany(item => item.ActionTable.Nodes[item.NodeIndex].NonTerminalActions).Select(item => item.Name).Distinct();
		}
		public IEnumerable<T> GetNextTerminals(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.ActionTable.Nodes[item.NodeIndex].TerminalActions).Select(item => item.Value).Distinct();
		}

	

		public IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, T Value)
		{
			Situation<T> newSituation;
			List<Situation<T>> results;

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				foreach (ShiftOnTerminal<T> action in situation.ActionTable.Nodes[situation.NodeIndex].TerminalActions)
				{
					if (!action.Match(Value)) continue;
					newSituation = new Situation<T>() { ActionTable = situation.ActionTable, NodeIndex = action.TargetNodeIndex };
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
				foreach (ShifOnNonTerminal<T> action in situation.ActionTable.Nodes[situation.NodeIndex].NonTerminalActions)
				{
					if (action.Name!=Name) continue;
					newSituation = new Situation<T>() { ActionTable = situation.ActionTable, NodeIndex = action.TargetNodeIndex };
					results.Add(newSituation);
				}
			}

			return results.Distinct();
		}

		
	}
}
