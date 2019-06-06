using FSMLib.Table;
using FSMLib.Actions;
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
	public class MockedAutomatonTable:AutomatonTable<char>
	{
		public MockedAutomatonTable(params char[] Values)
		{

			States.Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				States.Add(new State<char>());
				States[t].ShiftActions.Add(new Shift<char>() { Input = new TerminalInput<char>() { Value = Values[t] }, TargetStateIndex = t+1 });
			}

		}
		public MockedAutomatonTable(params string[] Values)
		{
			States.Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				States.Add(new State<char>());
				States[t].ShiftActions.Add(new Shift<char>() { Input = new NonTerminalInput<char>() { Name= Values[t] } , TargetStateIndex = t + 1 });
			}
		}

	}
}
