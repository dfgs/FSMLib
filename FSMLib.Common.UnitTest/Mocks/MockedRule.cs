﻿using FSMLib.Attributes;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedRule : IRule<char>
	{
		public string Name => "Mocked";

		public bool IsAxiom => false;

		private static MockedPredicate predicate = new MockedPredicate();
		public IPredicate<char> Predicate => predicate;

		public IEnumerable<IRuleAttribute> Attributes => throw new NotImplementedException();

		public bool Equals(IRule<char> other)
		{
			return other is MockedRule;
		}

		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			return "Mocked";
		}
	}
}
