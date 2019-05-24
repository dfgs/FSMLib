using FSMLib.Table;
using FSMLib.Table.Actions;
using FSMLib.Predicates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public  interface ISegmentFactory<T>
	{
		Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context,  BasePredicate<T> Predicate,IEnumerable<BaseAction<T>> OutActions);
	}
	public interface ISegmentFactory<TPredicate,T>: ISegmentFactory<T>
		where TPredicate:BasePredicate<T>
	{
		Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context,  TPredicate Predicate, IEnumerable<BaseAction<T>> OutActions);
	}
}
