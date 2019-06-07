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
	public static class SituationCollectionFactoryHelper
	{
		public static SituationCollectionFactory<char> BuildSituationCollectionFactory(IEnumerable<Rule<char>> Rules,IEnumerable<char> Alphabet)
		{
			return new SituationCollectionFactory<char>(SituationGraphHelper.BuildSituationGraph(Rules, Alphabet));
		}


	}
}
