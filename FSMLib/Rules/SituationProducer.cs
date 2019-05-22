using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;

namespace FSMLib.Rules
{
	public class SituationProducer<T> : ISituationProducer<T>
	{
		/*public IEnumerable<Transition<T>> GetAllTransitions(IEnumerable<Situation<T>> Situations)
		{
			return ;
		}*/

		

		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].NonTerminalTransitions).Select(item => item.Name).Distinct();
		}
		public IEnumerable<T> GetNextTerminals(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].TerminalTransitions).Select(item => item.Value).Distinct();
		}

	

		public IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations, T Value)
		{
			Situation<T> newSituation;
			List<Situation<T>> results;

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				foreach (TerminalTransition<T> transition in situation.Graph.Nodes[situation.NodeIndex].TerminalTransitions)
				{
					if (!transition.Match(Value)) continue;
					newSituation = new Situation<T>() { Graph = situation.Graph, NodeIndex = transition.TargetNodeIndex };
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
				foreach (NonTerminalTransition<T> transition in situation.Graph.Nodes[situation.NodeIndex].NonTerminalTransitions)
				{
					if (transition.Name!=Name) continue;
					newSituation = new Situation<T>() { Graph = situation.Graph, NodeIndex = transition.TargetNodeIndex };
					results.Add(newSituation);
				}
			}

			return results.Distinct();
		}

		
	}
}
