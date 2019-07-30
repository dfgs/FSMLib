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

namespace FSMLib.LexicalAnalysis.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class AutomatonTableParser
	{
		private AutomatonTable<char> automatonTable;
		private int stateIndex;


		public int ActionCount
		{
			get { return automatonTable.States[stateIndex].ShiftActionCount; }
		}

		
		public AutomatonTableParser(AutomatonTable<char> AutomatonTable)
		{
			this.automatonTable = AutomatonTable;this.stateIndex = 0;

		}

		

		public bool Parse(char Input)
		{
			Shift<char> action;
			IActionInput<char> terminalInput;

			terminalInput = new LetterInput( Input );
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
