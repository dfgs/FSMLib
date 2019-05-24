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

			Nodes.Add(new Node<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Nodes.Add(new Node<char>());
				Nodes[t].TerminalActions.Add(new ShiftOnTerminal<char>() { Value = Values[t], TargetNodeIndex = t+1 });
			}

		}
		public MockedActionTable(params string[] Values)
		{
			Nodes.Add(new Node<char>());

			for (int t = 0; t < Values.Length; t++)
			{
				Nodes.Add(new Node<char>());
				Nodes[t].NonTerminalActions.Add(new ShifOnNonTerminal<char>() { Name = Values[t], TargetNodeIndex = t + 1 });
			}
		}

	}
}
