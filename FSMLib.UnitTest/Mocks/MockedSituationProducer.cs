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

		public IEnumerable<Situation2<char>> Develop(ISituationGraph<char> SituationGraph, IEnumerable<Situation2<char>> Situations, IEnumerable<Rule<char>> Rules)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation2<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation2<char>> GetNextSituations(ISituationGraph<char> SituationGraph, IEnumerable<Situation2<char>> Situations, IInput<char> Input)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseTerminalInput<char>> GetNextTerminalInputs(IEnumerable<Situation2<char>> Situations)
		{
			throw new NotImplementedException();
		}
	}
}
