using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedAutomatonTableFactoryContext : IAutomatonTableFactoryContext<char>
	{
		public Segment<char> BuildSegment(Rule<char> Rule, IEnumerable<BaseAction<char>> OutActions)
		{
			throw new NotImplementedException();
		}

		public void Connect(IEnumerable<State<char>> States, IEnumerable<BaseAction<char>> Actions)
		{
			throw new NotImplementedException();
		}

		public State<char> CreateState()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetAlphabet()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseTerminalInput<char>> GetFirstTerminalInputsAfterAction(ShiftOnNonTerminal<char> Action)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseTerminalInput<char>> GetFirstTerminalInputsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Reduce<char>> GetReductionActions(string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public int GetStateIndex(State<char> State)
		{
			throw new NotImplementedException();
		}

		public State<char> GetTargetState(int Index)
		{
			throw new NotImplementedException();
		}
	}
}
