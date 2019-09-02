using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedRule2 : IRule<char>
	{
		public string Name => "Mocked";

		public bool IsAxiom => false;

		private static MockedPredicate2 predicate = new MockedPredicate2();
		public IPredicate<char> Predicate => predicate;

		public bool Equals(IRule<char> other)
		{
			return other is MockedRule2;
		}

		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return "Mocked";
		}
	}
}
