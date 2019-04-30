using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs.Inputs;

namespace FSMLib
{
	public class SituationProducer<T> : ISituationProducer<T>
	{

		public IEnumerable<IInput<T>> GetDistinctInputs(IEnumerable<Situation<T>> Situations)
		{
			// distinct uses GetHashCode to compare items
			return Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].Transitions).Select(item => item.Input).Distinct( );
		}
	}
}
