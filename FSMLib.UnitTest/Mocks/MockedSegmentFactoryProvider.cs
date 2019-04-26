using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedSegmentFactoryProvider<T> : ISegmentFactoryProvider<T>
	{
		public ISegmentFactory<T> GetSegmentFactory(RulePredicate<T> Predicate)
		{
			throw new NotImplementedException();
		}
	}
}
