using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Situations;
using FSMLib.LexicalAnalysis.Situations;

namespace FSMLib.Helpers
{
	public static class AutomatonTableHelper
	{
		public static AutomatonTable<char> BuildAutomatonTable(IEnumerable<string> Rules)
		{
			AutomatonTableFactory<char> automatonTableFactory;
			AutomatonTable<char> automatonTable;
			SituationCollectionFactory situationCollectionFactory;

			automatonTableFactory = new AutomatonTableFactory<char>( );
			situationCollectionFactory = new SituationCollectionFactory(SituationGraphHelper.BuildSituationGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray()));
			automatonTable = automatonTableFactory.BuildAutomatonTable( situationCollectionFactory,(char)0,(char)255);
		
			return automatonTable;

		}

		
	}
}
