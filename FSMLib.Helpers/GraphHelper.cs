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

			automatonTableFactory = new AutomatonTableFactory<char>( );

			automatonTable = automatonTableFactory.BuildAutomatonTable(SituationGraphHelper.BuildSituationGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet));
		
			return automatonTable;

		}

		
	}
}
