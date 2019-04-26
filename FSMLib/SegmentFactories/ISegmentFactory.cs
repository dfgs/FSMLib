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
		Segment BuildSegment(RulePredicate<T> Predicate);
	}
	public interface ISegmentFactory<TPredicate,T>: ISegmentFactory<T>
		where TPredicate:RulePredicate<T>
	{
		Segment BuildSegment(TPredicate Predicate);
	}
}
