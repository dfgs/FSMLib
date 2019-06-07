using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedSituationCollectionFactory : ISituationCollectionFactory<char>
	{
		private static SituationPredicate<char> predicate=new AnyTerminal<char>();
		public SituationPredicate<char> MockedPredicate
		{
			get => predicate;
		}

		

		public MockedSituationCollectionFactory()
		{
		}
		public bool Contains(SituationPredicate<char> Predicate)
		{
			return true;
		}

		
		public IEnumerable<SituationPredicate<char>> GetRuleInputPredicates(BasePredicate<char> RootPredicate)
		{
			throw new NotImplementedException();
		}

		

		public bool CanReduce(SituationPredicate<char> CurrentPredicate)
		{
			return true;
		}

		public IEnumerable<BaseTerminalInput<char>> GetInputsAfterPredicate(BasePredicate<char> CurrentPredicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SituationPredicate<char>> GetRuleInputPredicates(string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseInput<char>> GetInputsAfterPredicate(SituationPredicate<char> CurrentPredicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> CreateNextSituations(IEnumerable<Situation<char>> CurrentSituations, IInput<char> Input)
		{
			return Enumerable.Empty<Situation<char>>();
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

		public IEnumerable<Situation<char>> CreateAxiomSituations()
		{
			throw new NotImplementedException();
		}
	}
}
