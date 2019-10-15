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
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.Helpers
{
	public static class SituationCollectionFactoryHelper
	{
		public static ISituationCollectionFactory<Token> BuildSituationCollectionFactory(IEnumerable<IRule<Token>> Rules)
		{
			return new SituationCollectionFactory<Token>(SituationGraphHelper.BuildSituationGraph(Rules));
		}


	}
}
