using FSMLib.Common.Automatons;
using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis.Automatons
{
	public class Automaton : Automaton<Token>
	{
		public Automaton(IAutomatonTable<Token> AutomatonTable) : base(AutomatonTable)
		{
		}

		protected override INonTerminalInput<Token> OnCreateNonTerminalInput(string Name)
		{
			return new NonTerminalInput(Name);
		}

		protected override ITerminalInput<Token> OnCreateTerminalInput(Token Value)
		{
			return new TerminalInput(Value);
		}
	}
}
