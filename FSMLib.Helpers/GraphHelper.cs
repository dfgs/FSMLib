using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Situations;
using FSMLib.LexicalAnalysis.Situations;
using FSMLib.LexicalAnalysis.Tables;

namespace FSMLib.Helpers
{
	public static class AutomatonTableHelper
	{
		public static AutomatonTable<char> BuildAutomatonTable(IEnumerable<string> Rules)
		{
			AutomatonTableFactory<char> automatonTableFactory;
			AutomatonTable<char> automatonTable;
			SituationCollectionFactory<char> situationCollectionFactory;
			DistinctInputFactory distinctInputFactory;

			distinctInputFactory = new DistinctInputFactory();
			automatonTableFactory = new AutomatonTableFactory<char>( );
			situationCollectionFactory = new SituationCollectionFactory<char>(SituationGraphHelper.BuildSituationGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray()));
			automatonTable = automatonTableFactory.BuildAutomatonTable( situationCollectionFactory, distinctInputFactory);
		
			return automatonTable;

		}

		
	}
}
