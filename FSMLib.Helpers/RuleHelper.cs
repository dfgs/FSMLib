using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace FSMLib.Helpers
{
	public static class RuleHelper
	{
		public static Rule<char> BuildRule(string Pattern)
		{
			return RuleGrammar.Rule.Parse(Pattern);
		}

		
	}
}
