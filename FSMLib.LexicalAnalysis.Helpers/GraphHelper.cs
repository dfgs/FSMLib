using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Situations;
using FSMLib.LexicalAnalysis.Tables;
using FSMLib.Common.Table;
using FSMLib.Common.Situations;

namespace FSMLib.LexicalAnalysis.Helpers
{
	public static class AutomatonTableHelper
	{
		public static IAutomatonTable<char> BuildAutomatonTable(IEnumerable<string> Rules)
		{
			IAutomatonTableFactory<char> automatonTableFactory;
			IAutomatonTable<char> automatonTable;
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
