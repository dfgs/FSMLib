﻿using FSMLib.ActionTables.Actions;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public interface IActionTableFactoryContext<T>
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

		IEnumerable<T> GetFirstTerminalsForRule(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<T> GetFirstTerminalsAfterAction(State<T> State, string Name);

		//IEnumerable<Segment<T>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<T>> Rules, string Name);

		IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<Reduce<T>> GetReductionActions(string Name);

	}
}
