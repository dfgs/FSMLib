using FSMLib.Inputs;
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

		
		public IEnumerable<InputPredicate<char>> GetRuleInputPredicates(BasePredicate<char> RootPredicate)
		{
			throw new NotImplementedException();
		}

		

		public bool CanReduce(InputPredicate<char> CurrentPredicate)
		{
			return true;
		}

		public IEnumerable<BaseTerminalInput<char>> GetInputsAfterPredicate(BasePredicate<char> CurrentPredicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<InputPredicate<char>> GetRuleInputPredicates(string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseInput<char>> GetInputsAfterPredicate(InputPredicate<char> CurrentPredicate)
		{
			throw new NotImplementedException();
		}

		IEnumerable<BaseInput<char>> ISituationGraph<char>.GetInputsAfterPredicate(BasePredicate<char> CurrentPredicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> GetNextSituations(Situation<char> CurrentSituation)
		{
			return null;// predicate.AsEnumerable();
		}

		public bool Contains(BasePredicate<char> Predicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TerminalInput<char>> GetTerminalsAfterPredicate(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<NonTerminalInput<char>> GetNonTerminalsAfterPredicate(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public ISituationCollection<char> Develop(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> GetRootSituations()
		{
			throw new NotImplementedException();
		}
	}
}
