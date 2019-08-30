using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Situations
{
	public class SituationGraph<T>:ISituationGraph<T>
	{
		private List<ISituationNode<T>> nodes;
		public IEnumerable<ISituationNode<T>> Nodes
		{
			get { return nodes; }
		}
		
		
		public SituationGraph()
		{
			nodes = new List<ISituationNode<T>>();	
		}
		public void Add(ISituationNode<T> Node)
		{
			nodes.Add(Node);
		}
		public bool Contains(ISituationPredicate<T> Predicate)
		{
			ISituationEdge<T> egde;

			egde = Nodes.SelectMany(item => item.Edges).FirstOrDefault(item => item.Predicate == Predicate);

			return (egde != null);
		}

	}
}
