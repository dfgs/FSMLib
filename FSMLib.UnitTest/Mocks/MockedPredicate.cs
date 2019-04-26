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

		public override string ToParenthesisString(RulePredicate<T> Current)
		{
			throw new System.NotImplementedException();
		}

		public override string ToString(RulePredicate<T> Current)
		{
			throw new System.NotImplementedException();
		}
	}
}
