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
	public class MockedPredicate<T> : BasePredicate<T>
	{
		

		public override string ToString(BasePredicate<T> CurrentPredicate)
		{
			throw new System.NotImplementedException();
		}

	}
}
