using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Common.Situations;
using FSMLib.Common.Table;

namespace FSMLib.Common.UnitTest.Situations
{
	
	[TestClass]
	public class SituationDictionaryUnitTest
	{
		

		
		[TestMethod]

		public void ShouldNotGetTuple()
		{
			SituationDictionary<char> dictionary;
			IAutomatonTableTuple<char> result;
			IRule<char> rule;
			Situation<char> a,b;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[1] as Terminal };

			situations = new SituationCollection<char>();
			situations.Add(a);situations.Add(b);

			dictionary = new SituationDictionary<char>();
			result = dictionary.GetTuple(situations);
			Assert.IsNull(result);
		}

		[TestMethod]

		public void ShouldCreateAndGetTuple()
		{
			SituationDictionary<char> dictionary;
			IAutomatonTableTuple<char> result;
			IRule<char> rule;
			Situation<char> a, b;
			SituationCollection<char> situations;


			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[1] as Terminal };

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);

			dictionary = new SituationDictionary<char>();
			result = dictionary.GetTuple(situations);
			Assert.IsNull(result);
			dictionary.CreateTuple(new State<char>(),situations);
			result = dictionary.GetTuple(situations);
			Assert.IsNotNull(result);

		}

		[TestMethod]

		public void ShouldEnumerateTuples()
		{
			SituationDictionary<char> dictionary;
			IAutomatonTableTuple<char>[] result;
			IRule<char> rule;
			Situation<char> a, b;
			SituationCollection<char> situations;


			rule = RuleHelper.BuildRule("A=abc;");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[0] as Terminal };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as Sequence).Items[1] as Terminal };

			dictionary = new SituationDictionary<char>();

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			dictionary.CreateTuple(new State<char>(), situations);

			situations = new SituationCollection<char>();
			situations.Add(a); 
			dictionary.CreateTuple(new State<char>(), situations);

			situations = new SituationCollection<char>();
			situations.Add(b);
			dictionary.CreateTuple(new State<char>(), situations);

			result = dictionary.ToArray();
			Assert.AreEqual(3, result.Length);
		}



	}
}
