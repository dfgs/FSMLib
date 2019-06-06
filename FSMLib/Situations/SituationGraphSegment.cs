using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationGraphSegment<T>
	{
		

		public IEnumerable<SituationEdge<T>> InputEdges
		{
			get;
			set;
		}

		public IEnumerable<SituationNode<T>> OutputNodes
		{
			get;
			set;
		}

		public SituationGraphSegment()
		{
			
		}
	}
}
