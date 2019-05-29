using FSMLib.Predicates;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedSituationGraph : ISituationGraph<char>
	{
		private static InputPredicate<char> predicate=new AnyTerminal<char>();
		public InputPredicate<char> MockedPredicate
		{
			get => predicate;
		}

		public MockedSituationGraph()
		{
		}
		public bool Contains(InputPredicate<char> Predicate)
		{
			return true;
		}

		public IEnumerable<InputPredicate<char>> GetNextPredicates(InputPredicate<char> CurrentPredicate)
		{
			return predicate.AsEnumerable();
		}

		public IEnumerable<InputPredicate<char>> GetRootInputPredicates(BasePredicate<char> RootPredicate)
		{
			throw new NotImplementedException();
		}

		

		public bool CanReduce(InputPredicate<char> CurrentPredicate)
		{
			return true;
		}
	}
}
