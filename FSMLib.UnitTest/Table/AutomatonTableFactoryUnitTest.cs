﻿using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.UnitTest.Mocks;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Tables;

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
			LexicalRule rule;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>(  ) ;

			rule = RuleHelper.BuildRule("A*=abc;");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory( rule.AsEnumerable()),new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser(automatonTable);

			Assert.AreEqual(1, parser.ActionCount);
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
			LexicalRule rule1,rule2;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>(  );

			rule1 = RuleHelper.BuildRule("A*=abc;");
			rule2 = RuleHelper.BuildRule("B*=abc;");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new LexicalRule[] { rule1,rule2 }), new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser(automatonTable);

			
			Assert.AreEqual(1, parser.ActionCount);
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
			LexicalRule rule1, rule2;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>();

			rule1 = RuleHelper.BuildRule("A*=a.c;");
			rule2 = RuleHelper.BuildRule("B*=abc;");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new LexicalRule[] { rule1, rule2 }), new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser(automatonTable);


			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(3, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));

			parser.Reset();

			Assert.AreEqual(1, parser.ActionCount);
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
			LexicalRule rule;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>( );

			rule = RuleHelper.BuildRule("A*=a|b|c;");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(rule.AsEnumerable()), new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);
	
			parser = new AutomatonTableParser(automatonTable);

			Assert.AreEqual(3, parser.ActionCount);
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
			LexicalRule rule;
			Sequence predicate;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>( );

			predicate = new Sequence();
			predicate.Items.Add(new Terminal('a'));
			predicate.Items.Add(new Terminal('b'));
			predicate.Items.Add(new Terminal('c'));

			rule = new LexicalRule() { Name="rule",IsAxiom=true };
			rule.Predicate = new ZeroOrMore() { Item = predicate };

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(rule.AsEnumerable()), new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);
	
			parser = new AutomatonTableParser(automatonTable);

			Assert.AreEqual(1, parser.ActionCount);
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
			LexicalRule rule1, rule2;
			AutomatonTableParser parser;

			factory = new AutomatonTableFactory<char>();

			rule1 = RuleHelper.BuildRule("A*=abc;");
			rule2 = RuleHelper.BuildRule("A*=abc;");

			automatonTable = factory.BuildAutomatonTable(SituationCollectionFactoryHelper.BuildSituationCollectionFactory(new LexicalRule[] { rule1, rule2 }), new DistinctInputFactory());
			Assert.IsNotNull(automatonTable);

			parser = new AutomatonTableParser(automatonTable);


			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
		}





	}


}
