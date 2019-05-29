﻿using FSMLib.Actions;
using FSMLib.Inputs;
using FSMLib.Rules;
using FSMLib.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationProducer<T>
	{
		IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation<T>> Situations);
		//IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<T>> Situations);
		ISituationCollection<T> GetNextSituations(ISituationGraph<T> SituationGraph, IEnumerable<Situation<T>> Situations, IInput<T> Input);
		void Connect(IEnumerable<State<T>> States, IEnumerable<BaseAction<T>> Actions);
		ISituationCollection<T> Develop(ISituationGraph<T> SituationGraph,IEnumerable<Situation<T>> Situations,IEnumerable<Rule<T>> Rules);
	}
}
