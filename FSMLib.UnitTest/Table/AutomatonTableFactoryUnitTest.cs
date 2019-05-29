using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;

using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;

namespace FSMLib.UnitTest.AutomatonTables
{
	[TestClass]
	public class AutomatonTableFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AutomatonTableFactory<char>(null));
		}
		

		[TestMethod]
		public void ShouldBuildAutomatonTableFromBasicSequence()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>( new SituationProducer<char>() ) ;

			rule = RuleHelper.BuildRule("A=abc");

			automatonTable = factory.BuildAutomatonTable(rule.AsEnumerable(), new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser<char>(automatonTable);

			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
			
		}
		[TestMethod]
		public void ShouldBuildAutomatonTableFromTwoSequences()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule1,rule2;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>(  new SituationProducer<char>());

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");

			automatonTable = factory.BuildAutomatonTable(new Rule<char>[] { rule1,rule2 },new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser<char>(automatonTable);

			
			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
	
			

		}

		[TestMethod]
		public void ShouldBuildAutomatonTableFromBasicOr()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>(  new SituationProducer<char>());

			rule = RuleHelper.BuildRule("A=a|b|c");

			automatonTable = factory.BuildAutomatonTable(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(automatonTable);
	
			parser = new AutomatonTableParser<char>(automatonTable);

			Assert.AreEqual(4, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			parser.Reset();
			Assert.IsTrue(parser.Parse('b'));
			parser.Reset();
			Assert.IsTrue(parser.Parse('c'));


		}

		[TestMethod]
		public void ShouldBuildAutomatonTableFromExtendedSequence()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule;
			Sequence<char> predicate;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>(  new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>() { Name="rule" };
			rule.Predicate = new ZeroOrMore<char>() { Item = predicate };

			automatonTable = factory.BuildAutomatonTable(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(automatonTable);
	
			parser = new AutomatonTableParser<char>(automatonTable);

			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
		}
		[TestMethod]
		public void ShouldNotBuildAutomatonTableWhenNullRulesAreProvided()
		{
			AutomatonTableFactory<char> factory;

			factory = new AutomatonTableFactory<char>(  new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(()=> factory.BuildAutomatonTable(null,new char[] { 'a', 'b', 'c' }));
		}
		[TestMethod]
		public void ShouldNotBuildAutomatonTableWhenNullAlphabetIsProvided()
		{
			Rule<char> rule;
			AutomatonTableFactory<char> factory;

			
			rule = new Rule<char>() { Name = "rule" };
	
			factory = new AutomatonTableFactory<char>( new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildAutomatonTable(rule.AsEnumerable(), null));
		}
	


	

	


	}


}
