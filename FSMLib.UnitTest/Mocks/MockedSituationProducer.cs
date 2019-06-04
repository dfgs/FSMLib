using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Actions;
using FSMLib.Inputs;
using FSMLib.Rules;
using FSMLib.Situations;
using FSMLib.Table;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedSituationProducer : ISituationProducer<char>
	{
		public void Connect(IEnumerable<State<char>> States, IEnumerable<BaseAction<char>> Actions)
		{
			throw new NotImplementedException();
		}

		public void Connect(IEnumerable<State<char>> States, IEnumerable<Shift<char>> Actions)
		{
			throw new NotImplementedException();
		}

		public ISituationCollection<char> Develop(ISituationGraph<char> SituationGraph, IEnumerable<Situation<char>> Situations, IEnumerable<Rule<char>> Rules)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<NonTerminalInput<char>> GetNextNonTerminalInputs(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public ISituationCollection<char> GetNextSituations(ISituationGraph<char> SituationGraph, IEnumerable<Situation<char>> Situations, IInput<char> Input)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseTerminalInput<char>> GetNextTerminalInputs(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}
	}
}
