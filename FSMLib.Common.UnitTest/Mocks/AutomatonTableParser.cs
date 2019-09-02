using FSMLib.Table;
using FSMLib.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;

namespace FSMLib.Common.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class AutomatonTableParser
	{
		private IAutomatonTable<char> automatonTable;
		private int stateIndex;


		public int ActionCount
		{
			get { return automatonTable.GetState(stateIndex).ShiftActionCount; }
		}

		
		public AutomatonTableParser(IAutomatonTable<char> AutomatonTable)
		{
			this.automatonTable = AutomatonTable;this.stateIndex = 0;

		}

		

		public bool Parse(IActionInput<char> Input)
		{
			IShift<char> action;

			action = automatonTable.GetState(stateIndex).GetShift(Input);
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
