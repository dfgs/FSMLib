using FSMLib.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedPredicate2 : IReducePredicate<char>
	{
		public bool Equals(IPredicate<char> other)
		{
			return other is MockedPredicate2;
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			yield return new MockedReduceInput2();
		}

		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return "Mocked";
		}
	}
}
