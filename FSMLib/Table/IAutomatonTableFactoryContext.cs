using FSMLib.Actions;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;

namespace FSMLib.Table
{
	public interface IAutomatonTableFactoryContext<T>
	{
		/*IEnumerable<Rule<T>> Rules
		{
			get;
		}*/

		Segment<T> BuildSegment( Rule<T> Rule, IEnumerable<BaseAction<T>> OutActions);
		void Connect(IEnumerable<State<T>> States, IEnumerable<BaseAction<T>> Actions);
		State<T> GetTargetState(int Index);
		State<T> CreateState();
		int GetStateIndex(State<T> State);

		IEnumerable<T> GetAlphabet();

		IEnumerable<TerminalInput<T>> GetFirstTerminalInputsForRule(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<TerminalInput<T>> GetFirstTerminalInputsAfterAction( ShiftOnNonTerminal<T> Action);

		//IEnumerable<Segment<T>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<T>> Rules, string Name);

		IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<Reduce<T>> GetReductionActions(string Name);

	}
}
