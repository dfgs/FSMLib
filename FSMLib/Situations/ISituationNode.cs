using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationNode<T>
	{
		IRule<T> Rule
		{
			get;
		}

		IEnumerable<ISituationEdge<T>> Edges
		{
			get;
		}

		void Add(ISituationEdge<T> Edge);
		void Add(IEnumerable<ISituationEdge<T>> Edges);
	}
}
