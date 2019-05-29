﻿using System;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Rules
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

			rule = new Rule<char>() {Name="A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abcd)", rule.ToString());
		}

		[TestMethod]
		public void ShouldBeHumanReadableWithBullet()
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

			rule = new Rule<char>() { Name = "A" };
			rule.Predicate = predicate;

			Assert.AreEqual("A=(abc•d)", rule.ToString(item));

			Assert.AreEqual("A=(abcd)¤", rule.ToString(ReducePredicate<char>.Instance));
		}

	}
}
