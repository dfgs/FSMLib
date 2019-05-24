using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	
	[ExcludeFromCodeCoverage]
	public class MockedActionTable:ActionTable<char>
	{
		public MockedActionTable(params char[] Values)
		{

			States.Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				States.Add(new State<char>());
				States[t].TerminalActions.Add(new ShiftOnTerminal<char>() { Value = Values[t], TargetStateIndex = t+1 });
			}

		}
		public MockedActionTable(params string[] Values)
		{
			States.Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				States.Add(new State<char>());
				States[t].NonTerminalActions.Add(new ShifOnNonTerminal<char>() { Name = Values[t], TargetStateIndex = t + 1 });
			}
		}

	}
}
