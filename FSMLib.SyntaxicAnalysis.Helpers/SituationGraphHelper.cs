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
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.Helpers
{
	public static class SituationGraphHelper
	{
		public static ISituationGraph<Token> BuildSituationGraph(IEnumerable<IRule<Token>> Rules)
		{
			ISituationGraphFactory<Token> situationGraphFactory;
			IRule<Token>[] rules;

			rules = Rules.ToArray();
			
			situationGraphFactory = new SituationGraphFactory<Token>(new SituationGraphSegmentFactory<Token>());
			return situationGraphFactory.BuildSituationGraph( rules );
		}


	}
}
