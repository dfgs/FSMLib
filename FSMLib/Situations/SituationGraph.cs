using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraph<T>
	{
		public List<SituationNode<T>> Nodes
		{
			get;
			set;
		}
		
		
		public SituationGraph()
		{
			Nodes = new List<SituationNode<T>>();	
		}

		public bool Contains(ISituationPredicate<T> Predicate)
		{
			SituationEdge<T> egde;

			egde = Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == Predicate);

			return (egde != null);
		}

	}
}
