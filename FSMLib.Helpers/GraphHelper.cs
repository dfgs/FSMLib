﻿using FSMLib.Table;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
	public static class AutomatonTableHelper
	{
		public static AutomatonTable<char> BuildAutomatonTable(IEnumerable<string> Rules, IEnumerable<char> Alphabet)
		{
			AutomatonTableFactory<char> automatonTableFactory;
			AutomatonTable<char> automatonTable;

			automatonTableFactory = new AutomatonTableFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			automatonTable = automatonTableFactory.BuildAutomatonTable(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet);
		
			return automatonTable;

		}

		public static AutomatonTable<char> BuildDeterminiticAutomatonTable(IEnumerable<string> Rules,IEnumerable<char> Alphabet)
		{
			AutomatonTableFactory<char> automatonTableFactory;
			AutomatonTable<char> automatonTable,detAutomatonTable;

			automatonTableFactory = new AutomatonTableFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			automatonTable=automatonTableFactory.BuildAutomatonTable( Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet );
			detAutomatonTable = automatonTableFactory.BuildDeterministicAutomatonTable(automatonTable);

			return detAutomatonTable;

		}
	}
}
