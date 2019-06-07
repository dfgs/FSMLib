using FSMLib.Table;
using FSMLib.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FSMLib.Situations;
using FSMLib.Predicates;

namespace FSMLib.Helpers
{
	public static class SituationGraphHelper
	{
		public static SituationGraph<char> BuildSituationGraph(IEnumerable<Rule<char>> Rules,IEnumerable<char> Alphabet)
		{
			Rule<char> acceptRule;
			List<Rule<char>> acceptRules;
			Sequence<char> sequence;
			NonTerminal<char> nonTerminal;
			SituationGraphFactory<char> situationGraphFactory;
			Rule<char>[] rules;

			rules = Rules.ToArray();

			acceptRules = new List<Rule<char>>();
			foreach (Rule<char> axiom in Rules.Take(1)) //Rules.Where(item => item.IsAxiom))
			{

				nonTerminal = new NonTerminal<char>() { Name = axiom.Name };
				sequence = new Sequence<char>();
				sequence.Items.Add(nonTerminal);
				sequence.Items.Add(new EOS<char>());

				acceptRule = new Rule<char>() { Name = "Axiom", IsAxiom=true };
				acceptRule.Predicate = sequence;
				acceptRules.Add(acceptRule);
			}

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			return situationGraphFactory.BuildSituationGraph( acceptRules.Concat(rules) );
		}


	}
}
