using FSMLib.Predicates;
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
		public override IEnumerable<BasePredicate<T>> Enumerate()
		{
			throw new System.NotImplementedException();
		}

		public override string ToParenthesisString()
		{
			throw new System.NotImplementedException();
		}

		public override string ToString()
		{
			throw new System.NotImplementedException();
		}
	}
}
