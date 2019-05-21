﻿using System;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class RuleUnitTest
	{
		[TestMethod]
		public void ShouldBeHumanReadable()
		{
			Rule<char> rule;
			Sequence<char> predicate;
			Terminal<char> item;

			predicate = new Sequence<char>();
			item = new Terminal<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new Terminal<char>() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new Rule<char>();
			rule.Predicate = predicate;

			Assert.AreEqual(predicate.ToString(), rule.ToString());
		}


	}
}