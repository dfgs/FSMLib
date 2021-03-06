﻿using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FSMLib.Situations;
using FSMLib.Predicates;
using FSMLib.Common.Situations;

namespace FSMLib.LexicalAnalysis.Helpers
{
	public static class SituationGraphHelper
	{
		public static ISituationGraph<char> BuildSituationGraph(IEnumerable<IRule<char>> Rules)
		{
			ISituationGraphFactory<char> situationGraphFactory;
			IRule<char>[] rules;

			rules = Rules.ToArray();
			
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			return situationGraphFactory.BuildSituationGraph( rules );
		}


	}
}
