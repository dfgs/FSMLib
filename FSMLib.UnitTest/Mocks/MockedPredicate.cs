using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedPredicate<T> : IPredicate<T>
	{
		public bool Equals(IPredicate<T> other)
		{
			throw new System.NotImplementedException();
		}

		public string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			throw new System.NotImplementedException();
		}

	}
}
