using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class AutomatonTableParser<T>
	{
		private AutomatonTable<T> automatonTable;
		private int stateIndex;

		public int ActionCount
		{
			get { return automatonTable.States[stateIndex].TerminalActions.Count+ automatonTable.States[stateIndex].NonTerminalActions.Count; }
		}

		
		public AutomatonTableParser(AutomatonTable<T> AutomatonTable)
		{
			this.automatonTable = AutomatonTable;this.stateIndex = 0;

		}

		public bool Parse(T Input, int MatchIndex = 0)
		{
			int index = 0;

			foreach(ShiftOnTerminal<T> action in automatonTable.States[stateIndex].TerminalActions)
			{
				if (action.Input.Value.Equals(Input))
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
