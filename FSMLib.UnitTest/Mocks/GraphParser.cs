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
		private int stateIndex;

		public int ActionCount
		{
			get { return actionTable.States[stateIndex].TerminalActions.Count+ actionTable.States[stateIndex].NonTerminalActions.Count; }
		}

		
		public ActionTableParser(ActionTable<T> ActionTable)
		{
			this.actionTable = ActionTable;this.stateIndex = 0;

		}

		public bool Parse(T Input, int MatchIndex = 0)
		{
			int index = 0;

			foreach(ShiftOnTerminal<T> action in actionTable.States[stateIndex].TerminalActions)
			{
				if (action.Match(Input))
				{
					if (index == MatchIndex)
					{
						stateIndex = action.TargetStateIndex;
						return true;
					}
					index++;
				}
			}
			return false;
		}

		public void Reset()
		{
			stateIndex = 0;
		}

	}
}
