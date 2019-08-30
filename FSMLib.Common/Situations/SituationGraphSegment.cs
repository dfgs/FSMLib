using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Situations
{
	public class SituationGraphSegment<T>: ISituationGraphSegment<T>
	{
		

		public IEnumerable<ISituationEdge<T>> InputEdges
		{
			get;
			set;
		}

		public IEnumerable<ISituationNode<T>> OutputNodes
		{
			get;
			set;
		}

		public SituationGraphSegment()
		{
			
		}
	}
}
