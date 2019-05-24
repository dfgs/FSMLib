using System;
using System.Linq;
using FSMLib.ActionTables;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.ActionTables
{
	[TestClass]
	public class ActionTableFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableFactory<char>( null, new MockedSituationProducer()));
			Assert.ThrowsException<ArgumentNullException>(() => new ActionTableFactory<char>( new MockedSegmentFactoryProvider<char>(),null));
		}
		

		[TestMethod]
		public void ShouldBuildActionTableFromBasicSequence()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> actionTable;
			Rule<char> rule;
			ActionTableParser<char> parser;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(),new SituationProducer<char>() ) ;

			rule = RuleHelper.BuildRule("A=abc");

			actionTable = factory.BuildActionTable(rule.AsEnumerable(), new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(4, actionTable.States.Count);

			parser = new ActionTableParser<char>(actionTable);

			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.ActionCount);

		}
		[TestMethod]
		public void ShouldBuildActionTableFromTwoSequences()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> actionTable;
			Rule<char> rule1,rule2;
			ActionTableParser<char> parser;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			rule1 = RuleHelper.BuildRule("A=abc");
			rule2 = RuleHelper.BuildRule("B=abc");

			actionTable = factory.BuildActionTable(new Rule<char>[] { rule1,rule2 },new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(7, actionTable.States.Count);

			parser = new ActionTableParser<char>(actionTable);

			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.ActionCount);

			parser.Reset();
			Assert.AreEqual(2, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a',1));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('b'));
			Assert.AreEqual(1, parser.ActionCount);
			Assert.IsTrue(parser.Parse('c'));
			Assert.AreEqual(0, parser.ActionCount);

		}

		[TestMethod]
		public void ShouldBuildActionTableFromBasicOr()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> actionTable;
			Rule<char> rule;
			ActionTableParser<char> parser;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			rule = RuleHelper.BuildRule("A=a|b|c");

			actionTable = factory.BuildActionTable(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(4, actionTable.States.Count);

			parser = new ActionTableParser<char>(actionTable);

			Assert.AreEqual(3, parser.ActionCount);
			Assert.IsTrue(parser.Parse('a'));
			parser.Reset();
			Assert.IsTrue(parser.Parse('b'));
			parser.Reset();
			Assert.IsTrue(parser.Parse('c'));


		}

		[TestMethod]
		public void ShouldBuildActionTableFromExtendedSequence()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> actionTable;
			Rule<char> rule;
			Sequence<char> predicate;
			ActionTableParser<char> parser;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>() { Name="rule" };
			rule.Predicate = new ZeroOrMore<char>() { Item = predicate };

			actionTable = factory.BuildActionTable(rule.AsEnumerable(),new char[] { 'a', 'b', 'c' });
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(4, actionTable.States.Count);

			parser = new ActionTableParser<char>(actionTable);

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
		public void ShouldNotBuildActionTableWhenNullRulesAreProvided()
		{
			ActionTableFactory<char> factory;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(()=> factory.BuildActionTable(null,new char[] { 'a', 'b', 'c' }));
		}
		[TestMethod]
		public void ShouldNotBuildActionTableWhenNullAlphabetIsProvided()
		{
			Rule<char> rule;
			ActionTableFactory<char> factory;

			
			rule = new Rule<char>() { Name = "rule" };
	
			factory = new ActionTableFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildActionTable(rule.AsEnumerable(), null));
		}
		[TestMethod]
		public void ShouldNotBuildDeterministicActionTableWhenNullParameterIsProvided()
		{
			ActionTableFactory<char> factory;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildDeterministicActionTable(null));
		}
		[TestMethod]
		public void ShouldBuildDeterministicActionTableFromEmptyBaseActionTable()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> actionTable;

			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			actionTable = factory.BuildDeterministicActionTable(new ActionTable<char>());
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(0, actionTable.States.Count);
		}


		[TestMethod]
		public void ShouldBuildDeterministicActionTableFromTestActionTable1()
		{
			ActionTableFactory<char> factory;
			ActionTable<char> baseActionTable;
			ActionTable<char> actionTable;

			baseActionTable = ActionTableHelper.BuildActionTable(new string[] { "A=abc", "B=abc" }, new char[] { 'a', 'b', 'c', 'd', 'e' });


			factory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			actionTable = factory.BuildDeterministicActionTable(baseActionTable);
			Assert.IsNotNull(actionTable);
			Assert.AreEqual(4, actionTable.States.Count);
			Assert.AreEqual(1, actionTable.States[0].TerminalActions.Count);
			Assert.AreEqual(1, actionTable.States[1].TerminalActions.Count);
			Assert.AreEqual(1, actionTable.States[2].TerminalActions.Count);
			Assert.AreEqual(0, actionTable.States[3].TerminalActions.Count);
			Assert.IsTrue(actionTable.States[0].TerminalActions[0].Match('a'));
			Assert.IsTrue(actionTable.States[1].TerminalActions[0].Match('b'));
			Assert.IsTrue(actionTable.States[2].TerminalActions[0].Match('c'));

			for (int t = 0; t < 3; t++) Assert.AreEqual(0, actionTable.States[t].ReductionActions.Count);

		}

	


	}


}
