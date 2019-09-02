using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedSituationEdge : ISituationEdge<char>
	{
		public IRule<char> Rule => throw new NotImplementedException();

		public ISituationPredicate<char> Predicate => throw new NotImplementedException();

		public ISituationNode<char> TargetNode => throw new NotImplementedException();
	}
}
