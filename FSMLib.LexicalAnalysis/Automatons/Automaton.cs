using FSMLib.Common.Automatons;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Automatons
{
	public class Automaton : Automaton<char>
	{
		public Automaton(IAutomatonTable<char> AutomatonTable) : base(AutomatonTable)
		{
		}

		protected override INonTerminalInput<char> OnCreateNonTerminalInput(string Name)
		{
			return new NonTerminalInput(Name);
		}

		protected override ITerminalInput<char> OnCreateTerminalInput(char Value)
		{
			return new TerminalInput(Value);
		}
	}
}
