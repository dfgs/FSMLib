﻿using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraphFactory<T>
	{
		SituationGraph<T> BuildSituationGraph(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet);
	}
}
