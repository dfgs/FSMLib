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
		ISituationGraph<T> BuildSituationGraph(IEnumerable<IRule<T>> Rules);
	}
}
