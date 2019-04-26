using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public interface ISegmentFactoryProvider<T>
	{
		ISegmentFactory<T> GetSegmentFactory(RulePredicate<T> Predicate);
	}
}
