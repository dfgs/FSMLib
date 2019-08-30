using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraph<T>
	{
		IEnumerable<ISituationNode<T>> Nodes
		{
			get;
		}

		void Add(ISituationNode<T> Node);

		bool Contains(ISituationPredicate<T> Predicate);
	}

}
