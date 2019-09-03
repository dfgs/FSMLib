using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FSMLib.LexicalAnalysis.Rules;

namespace FSMLib.LexicalAnalysis.Helpers
{
	public static class RuleHelper
	{
		public static LexicalRule BuildRule(string Pattern)
		{
			return RuleGrammar.Rule.Parse(Pattern);
		}

		
	}
}
