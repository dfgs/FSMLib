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
		public void ShouldBuildAutomatonTableFromBasicSequence()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>(  ) ;

			rule = RuleHelper.BuildRule("A=abc");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory( rule.AsEnumerable(), new char[] { 'a', 'b', 'c' }));
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

			factory = new AutomatonTableFactory<char>(  );

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new Rule<char>[] { rule1,rule2 },new char[] { 'a', 'b', 'c' }));
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
		public void ShouldBuildAutomatonTableFromTwoSequencesUsingAnyTerminal()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule1, rule2;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>();

			rule1 = RuleHelper.BuildRule("A=a.c");
			rule2 = RuleHelper.BuildRule("B=abc");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new Rule<char>[] { rule1, rule2 }, new char[] { 'a', 'b', 'c' }));
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser<char>(automatonTable);


			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(3, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));

			parser.Reset();

			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(3, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
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

			factory = new AutomatonTableFactory<char>( );

			rule = RuleHelper.BuildRule("A=a|b|c");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' }));
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

			factory = new AutomatonTableFactory<char>( );

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>() { Name="rule" };
			rule.Predicate = new ZeroOrMore<char>() { Item = predicate };

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' }));
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
		public void ShouldManageReductionConflict()
		{
			AutomatonTableFactory<char> factory;
			AutomatonTable<char> automatonTable;
			Rule<char> rule1, rule2;
			AutomatonTableParser<char> parser;

			factory = new AutomatonTableFactory<char>();

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("A=abc");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new Rule<char>[] { rule1, rule2 }, new char[] { 'a', 'b', 'c' }));
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser<char>(automatonTable);


			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
		}





	}


}
