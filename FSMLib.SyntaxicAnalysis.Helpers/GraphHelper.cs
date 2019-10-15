using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Situations;
using FSMLib.SyntaxicAnalysis.Tables;
using FSMLib.Common.Table;
using FSMLib.Common.Situations;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.Helpers
{
	public static class AutomatonTableHelper
	{
		public static IAutomatonTable<Token> BuildAutomatonTable(IEnumerable<string> Rules)
		{
			IAutomatonTableFactory<Token> automatonTableFactory;
			IAutomatonTable<Token> automatonTable;
			SituationCollectionFactory<Token> situationCollectionFactory;
			DistinctInputFactory distinctInputFactory;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			automatonTableFactory = new AutomatonTableFactory<Token>( );
			situationCollectionFactory = new SituationCollectionFactory<Token>(SituationGraphHelper.BuildSituationGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray()));
			automatonTable = automatonTableFactory.BuildAutomatonTable( situationCollectionFactory, distinctInputFactory);
		
			return automatonTable;

		}

		
	}
}
