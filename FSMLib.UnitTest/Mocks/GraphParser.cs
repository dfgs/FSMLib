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
	public class ActionTableParser<T>
	{
		private ActionTable<T> actionTable;
		private int nodeIndex;

		public int ActionCount
		{
			get { return actionTable.Nodes[nodeIndex].TerminalActions.Count+ actionTable.Nodes[nodeIndex].NonTerminalActions.Count; }
		}

		
		public ActionTableParser(ActionTable<T> ActionTable)
		{
			this.actionTable = ActionTable;this.nodeIndex = 0;

		}

		public bool Parse(T Input, int MatchIndex = 0)
		{
			int index = 0;

			foreach(ShiftOnTerminal<T> action in actionTable.Nodes[nodeIndex].TerminalActions)
			{
				if (action.Match(Input))
				{
					if (index == MatchIndex)
					{
						nodeIndex = action.TargetNodeIndex;
						return true;
					}
					index++;
				}
			}
			return false;
		}

		public void Reset()
		{
			nodeIndex = 0;
		}

	}
}
