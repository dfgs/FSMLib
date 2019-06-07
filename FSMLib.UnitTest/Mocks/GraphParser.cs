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
	public class AutomatonTableParser<T>
	{
		private AutomatonTable<T> automatonTable;
		private int stateIndex;


		public int ActionCount
		{
			get { return automatonTable.States[stateIndex].ShiftActionCount; }
		}

		
		public AutomatonTableParser(AutomatonTable<T> AutomatonTable)
		{
			this.automatonTable = AutomatonTable;this.stateIndex = 0;

		}

		

		public bool Parse(T Input)
		{
			Shift<T> action;
			IInput<T> terminalInput;

			terminalInput = new TerminalInput<T>() { Value = Input };
			action = automatonTable.States[stateIndex].GetShift(terminalInput);
			if (action == null) return false;

			stateIndex = action.TargetStateIndex;
			return true;

		}

		public void Reset()
		{
			stateIndex = 0;
		}

	}
}
