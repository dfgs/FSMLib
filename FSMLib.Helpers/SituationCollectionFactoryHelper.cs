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
using FSMLib.LexicalAnalysis.Situations;

namespace FSMLib.Helpers
{
	public static class SituationCollectionFactoryHelper
	{
		public static SituationCollectionFactory<char> BuildSituationCollectionFactory(IEnumerable<IRule<char>> Rules)
		{
			return new SituationCollectionFactory<char>(SituationGraphHelper.BuildSituationGraph(Rules));
		}


	}
}
