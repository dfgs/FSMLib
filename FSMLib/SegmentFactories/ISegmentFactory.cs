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
		Segment<T> BuildSegment( IGraphFactoryContext<T> Context,  BasePredicate<T> Predicate,IEnumerable<BaseTransition<T>> OutTransitions);
	}
	public interface ISegmentFactory<TPredicate,T>: ISegmentFactory<T>
		where TPredicate:BasePredicate<T>
	{
		Segment<T> BuildSegment( IGraphFactoryContext<T> Context,  TPredicate Predicate, IEnumerable<BaseTransition<T>> OutTransitions);
	}
}
