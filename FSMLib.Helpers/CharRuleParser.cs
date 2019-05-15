using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
	public class CharRuleParser : IRuleParser<char>
	{
		public Rule<char> Parse(string Rule)
		{
			return RuleGrammar.Rule.Parse(Rule);
		}

	}
}
