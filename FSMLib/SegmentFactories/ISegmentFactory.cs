using FSMLib.Graphs;
using FSMLib.Predicates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public  interface ISegmentFactory<T>
	{
		Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, RulePredicate<T> Predicate);
	}
	public interface ISegmentFactory<TPredicate,T>: ISegmentFactory<T>
		where TPredicate:RulePredicate<T>
	{
		Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, TPredicate Predicate);
	}
}
