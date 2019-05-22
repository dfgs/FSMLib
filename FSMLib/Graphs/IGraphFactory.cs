using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public interface IGraphFactory<T>
	{
		Graph<T> BuildGraph(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet);
		Graph<T> BuildDeterministicGraph(Graph<T> BaseGraph);

	}
}
