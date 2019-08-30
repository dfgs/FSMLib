using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Table;
using FSMLib.Common.Actions;

namespace FSMLib.UnitTest.Mocks
{
	
	[ExcludeFromCodeCoverage]
	public class MockedAutomatonTable:AutomatonTable<char>
	{
		public MockedAutomatonTable(params char[] Values)
		{

			Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Add(new State<char>());
				GetState(t).Add(new Shift<char>() { Input = new TerminalInput(Values[t] ), TargetStateIndex = t+1 });
			}

		}
		public MockedAutomatonTable(params string[] Values)
		{
			Add(new State<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Add(new State<char>());
				GetState(t).Add(new Shift<char>() { Input = new NonTerminalInput( Values[t]) , TargetStateIndex = t + 1 });
			}
		}

	}
}
