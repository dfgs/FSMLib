using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Situations;

namespace FSMLib.Helpers
{
	public static class AutomatonTableHelper
	{
		public static AutomatonTable<char> BuildAutomatonTable(IEnumerable<string> Rules, IEnumerable<char> Alphabet)
		{
			AutomatonTableFactory<char> automatonTableFactory;
			AutomatonTable<char> automatonTable;
			SituationCollectionFactory<char> situationCollectionFactory;

			automatonTableFactory = new AutomatonTableFactory<char>( );
			situationCollectionFactory = new SituationCollectionFactory<char>(SituationGraphHelper.BuildSituationGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet));
			automatonTable = automatonTableFactory.BuildAutomatonTable( situationCollectionFactory);
		
			return automatonTable;

		}

		
	}
}
