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
using FSMLib.Common.Situations;

namespace FSMLib.LexicalAnalysis.Helpers
{
	public static class SituationCollectionFactoryHelper
	{
		public static ISituationCollectionFactory<char> BuildSituationCollectionFactory(IEnumerable<IRule<char>> Rules)
		{
			return new SituationCollectionFactory<char>(SituationGraphHelper.BuildSituationGraph(Rules));
		}


	}
}
