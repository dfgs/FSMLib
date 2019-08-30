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
	public class SituationNode<T>:ISituationNode<T>
	{
		public IRule<T> Rule
		{
			get;
			set;
		}

		private List<ISituationEdge<T>> edges;
		public IEnumerable<ISituationEdge<T>> Edges
		{
			get { return edges; }
		}

		

		public SituationNode()
		{
			edges = new List<ISituationEdge<T>>();
		}

		public void Add(IEnumerable<ISituationEdge<T>> Edges)
		{
			edges.AddRange(Edges);
		}
		public void Add(ISituationEdge<T> Edge)
		{
			edges.Add(Edge);
		}


	}
}
