using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FSMLib.SyntaxicAnalysis.Rules;

namespace FSMLib.SyntaxicAnalysis.Helpers
{
	public static class RuleHelper
	{
		public static SyntaxicRule BuildRule(string Pattern)
		{
			return RuleGrammar.Rule.Parse(Pattern);
		}

		
	}
}
