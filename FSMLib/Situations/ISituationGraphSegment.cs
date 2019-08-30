using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraphSegment<T>
	{

		IEnumerable<ISituationEdge<T>> InputEdges
		{
			get;
			set;
		}

		IEnumerable<ISituationNode<T>> OutputNodes
		{
			get;
			set;
		}
	}
}
