using FSMLib.Predicates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedPredicate<T> : RulePredicate<T>
	{
		public override IEnumerable<RulePredicate<T>> Enumerate()
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
