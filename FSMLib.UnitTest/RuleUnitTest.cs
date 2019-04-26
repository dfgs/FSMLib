using System;
using FSMLib.Predicates;
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
			One<char> item;

			predicate = new Sequence<char>();
			item = new One<char>() { Value = 'a' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'b' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'c' };
			predicate.Items.Add(item);
			item = new One<char>() { Value = 'd' };
			predicate.Items.Add(item);

			rule = new Rule<char>();
			rule.Predicate = predicate;

			Assert.AreEqual(predicate.ToString(), rule.ToString());
		}


	}
}
