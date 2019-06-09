using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FSMLib.Situations;
using FSMLib.Predicates;

namespace FSMLib.Helpers
{
	public static class SituationGraphHelper
	{
		public static SituationGraph<char> BuildSituationGraph(IEnumerable<Rule<char>> Rules,IEnumerable<char> Alphabet)
		{
			SituationGraphFactory<char> situationGraphFactory;
			Rule<char>[] rules;

			rules = Rules.ToArray();
			
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			return situationGraphFactory.BuildSituationGraph( rules,Alphabet );
		}


	}
}
