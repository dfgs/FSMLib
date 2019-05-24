using FSMLib.ActionTables;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
	public static class ActionTableHelper
	{
		public static ActionTable<char> BuildActionTable(IEnumerable<string> Rules, IEnumerable<char> Alphabet)
		{
			ActionTableFactory<char> actionTableFactory;
			ActionTable<char> actionTable;

			actionTableFactory = new ActionTableFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			actionTable = actionTableFactory.BuildActionTable(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet);
		
			return actionTable;

		}

		public static ActionTable<char> BuildDeterminiticActionTable(IEnumerable<string> Rules,IEnumerable<char> Alphabet)
		{
			ActionTableFactory<char> actionTableFactory;
			ActionTable<char> actionTable,detActionTable;

			actionTableFactory = new ActionTableFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			actionTable=actionTableFactory.BuildActionTable( Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet );
			detActionTable = actionTableFactory.BuildDeterministicActionTable(actionTable);

			return detActionTable;

		}
	}
}
