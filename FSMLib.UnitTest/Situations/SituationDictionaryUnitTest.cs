﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Table;
using FSMLib.Helpers;

namespace FSMLib.UnitTest.Situations
{
	
	[TestClass]
	public class SituationDictionaryUnitTest
	{
		

		
		[TestMethod]

		public void ShouldNotGetTuple()
		{
			SituationDictionary<char> dictionary;
			AutomatonTableTuple<char> result;
			Rule<char> rule;
			Situation<char> a,b;
			SituationCollection<char> situations;

			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[0] as TerminalPredicate<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[1] as TerminalPredicate<char> };

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
			AutomatonTableTuple<char> result;
			Rule<char> rule;
			Situation<char> a, b;
			SituationCollection<char> situations;


			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[0] as TerminalPredicate<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[1] as TerminalPredicate<char> };

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
			AutomatonTableTuple<char>[] result;
			Rule<char> rule;
			Situation<char> a, b;
			SituationCollection<char> situations;


			rule = RuleHelper.BuildRule("A=abc");
			a = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[0] as TerminalPredicate<char> };
			b = new Situation<char>() { Rule = rule, Predicate = (rule.Predicate as SequencePredicate<char>).Items[1] as TerminalPredicate<char> };

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
