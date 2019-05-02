using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;

namespace FSMLib
{
	public class SituationProducer<T> : ISituationProducer<T>
	{
		/*public IEnumerable<Transition<T>> GetAllTransitions(IEnumerable<Situation<T>> Situations)
		{
			return ;
		}*/

		public IEnumerable<IInput<T>> GetNextInputs(IEnumerable<Situation<T>> Situations)
		{
			return Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].Transitions).Select(item => item.Input).DisctinctEx();
		}

		public IEnumerable<Situation<T>> GetNextSituations(IEnumerable<Situation<T>> Situations,IInput<T> Input)
		{
			Situation<T> newSituation;
			List<Situation<T>> results;

			results = new List<Situation<T>>();
			foreach (Situation<T> situation in Situations)
			{
				foreach (Transition<T> transition in situation.Graph.Nodes[situation.NodeIndex].Transitions)
				{
					if (!transition.Input.Match(Input)) continue;
					newSituation = new Situation<T>() { Graph = situation.Graph, NodeIndex = transition.TargetNodeIndex };
					results.Add(newSituation);
				}
			}

			return results.DisctinctEx();
		}



	}
}
