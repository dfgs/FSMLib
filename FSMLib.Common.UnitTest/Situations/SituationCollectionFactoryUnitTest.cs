using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Situations;
using FSMLib.Common;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Situations
{
	
	[TestClass]
	public class SituationCollectionFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SituationCollectionFactory<char>(null));
		}



		[TestMethod]
		public void ShouldCreateAxiomSituations()
		{
			Terminal a, b, c;
			Sequence predicate;
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			ISituation<char>[] items;
			LexicalRule rule1,rule2;
			SituationCollectionFactory<char> factory;

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule1 = new LexicalRule() { Name = "A", Predicate = predicate, IsAxiom = true };
			rule2 = new LexicalRule() { Name = "A", Predicate = predicate, IsAxiom = false };
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1,rule2});

			factory = new SituationCollectionFactory<char>(graph);

			items = factory.CreateAxiomSituations().ToArray();
			Assert.AreEqual(1,items.Length);
			Assert.AreEqual(rule1, items[0].Rule);
			Assert.AreEqual(a, items[0].Predicate);
		}


		[TestMethod]
		public void ShouldCreateNextSituations()
		{
			Terminal a, b, c;
			Sequence predicate;
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			ISituation<char>[] items;
			LexicalRule rule;
			ISituation<char> situation;
			SituationCollectionFactory<char> factory;

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);
			predicate.Items.Add(new Reduce());

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>(rule,a, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput( 'b')).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>(rule,a, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(),new TerminalInput('a')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>(rule,b, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput( 'b')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>(rule,c, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput( 'c')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Predicate.Equals(new Reduce()));
		}

		[TestMethod]
		public void ShouldCreateNextSituationsUsingLettersRange()
		{
			Terminal a, c;
			AnyTerminal b;
			Sequence predicate;
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			ISituation<char>[] items;
			LexicalRule rule;
			Situation<char> situation;
			SituationCollectionFactory<char> factory;

			a = new Terminal('a');
			b = new AnyTerminal();
			c = new Terminal('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);
			predicate.Items.Add(new Reduce());

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>(rule,a, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput('b')).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>(rule,a, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput('a')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>(rule,b, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput('b')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>(rule,b, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalsRangeInput(char.MinValue,char.MaxValue)).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>(rule,c, new MockedReduceInput());
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput('c')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Predicate.Equals(new Reduce()));
		}


		[TestMethod]
		public void ShouldDevelopSituations()
		{
			SituationGraphFactory<char> situationGraphFactory; 
			ISituationGraph<char> graph;
			LexicalRule rule1, rule2, rule3,rule4;
			Situation<char> situation;
			ISituationCollection<char> situations;
			NonTerminal p1, p2;
			Terminal p3, p4;
			Sequence sequence;
			SituationCollectionFactory<char> factory;

			p1 = new NonTerminal( "B" );
			p2 = new NonTerminal( "C" );
			p3 = new Terminal('b');
			p4 = new Terminal('c');

			sequence = new Sequence();
			sequence.Items.Add(p1);
			sequence.Items.Add(new Terminal('d'));

			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };
			rule2 = new LexicalRule() { Name = "B", Predicate = p2 };
			rule3 = new LexicalRule() { Name = "B", Predicate = p3 };
			rule4 = new LexicalRule() { Name = "C", Predicate = p4 };

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2, rule3, rule4 });
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>( rule1, p1, new MockedReduceInput());

			situations = factory.Develop(situation.AsEnumerable());


			Assert.AreEqual(4, situations.Count);
			Assert.AreEqual(p1, situations[0].Predicate);
			Assert.AreEqual(p2, situations[1].Predicate);
			Assert.AreEqual('d',((ITerminalInput<char>)situations[1].Input).Value);
			Assert.AreEqual(p3, situations[2].Predicate);
			Assert.AreEqual('d', ((ITerminalInput<char>)situations[2].Input).Value);
			Assert.AreEqual(p4, situations[3].Predicate);
			Assert.AreEqual('d', ((ITerminalInput<char>)situations[3].Input).Value);

		}

		[TestMethod]
		public void ShouldDevelopSituationEndingWithReductionOnNonTerminal()
		{
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			LexicalRule rule1, rule2;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Terminal p1;
			NonTerminal p2;
			Terminal  p3;
			Sequence sequence;
			SituationCollectionFactory<char> factory;

			p1 = new Terminal('a');
			p2 = new NonTerminal( "B");
			p3 = new Terminal('b');

			sequence = new Sequence();
			sequence.Items.Add(p1);
			sequence.Items.Add(p2);
			sequence.Items.Add(new Reduce());

			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };
			rule2 = new LexicalRule() { Name = "B", Predicate = p3 };

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2 });
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>( rule1,  p2, new MockedReduceInput());
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(2, situations.Count);
			Assert.AreEqual(p3, situations[1].Predicate);


		}

		[TestMethod]
		public void ShouldDevelopSituationWithLeftRecursiveReduction()
		{
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			LexicalRule rule1, rule2,rule3;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Terminal p1;
			NonTerminal p2;
			Terminal p3;
			Terminal p4;
			Sequence sequence;
			SituationCollectionFactory<char> factory;

			//A*=•{S}a
			//S=•{S}b 
			//S=•c

			p1 = new Terminal('a');
			p2 = new NonTerminal( "S" );
			p3 = new Terminal('b');
			p4 = new Terminal('c');

			sequence = new Sequence();
			sequence.Items.Add(p2);
			sequence.Items.Add(p1);
			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };

			sequence = new Sequence();
			sequence.Items.Add(p2);
			sequence.Items.Add(p3);
			rule2 = new LexicalRule() { Name = "S", Predicate = sequence };

			rule3 = new LexicalRule() { Name = "S", Predicate = p4 };

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2,rule3 });
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>( rule1,  p2, new MockedReduceInput());
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(5, situations.Count);
			Assert.AreEqual('a', ((ITerminalInput<char>)situations[1].Input).Value); 
			Assert.AreEqual('b', ((ITerminalInput<char>)situations[2].Input).Value); 
			Assert.AreEqual('a', ((ITerminalInput<char>)situations[3].Input).Value); 
			Assert.AreEqual('b', ((ITerminalInput<char>)situations[4].Input).Value); 



		}

		[TestMethod]
		public void ShouldDevelopSituationWithLoopReduction()
		{
			SituationGraphFactory<char> situationGraphFactory;
			ISituationGraph<char> graph;
			LexicalRule rule1, rule2, rule3;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Terminal p1;
			NonTerminal p2;
			NonTerminal p3;
			SituationCollectionFactory<char> factory;
			OneOrMore oneOrMore;

			//"L=a"
			//"N={L}+"
			//"A*={N}"

			p1 = new Terminal('a');
			p2 = new NonTerminal( "L" );
			p3 = new NonTerminal( "N" );

			rule1 = new LexicalRule() { Name = "L", Predicate =new Sequence( p1,new Reduce()) };

			oneOrMore = new OneOrMore();
			oneOrMore.Item = p2;
			rule2 = new LexicalRule() { Name = "N", Predicate = new Sequence(oneOrMore, new Reduce()) };

			rule3 = new LexicalRule() { Name = "A", Predicate = new Sequence(p3, new Reduce()) };

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2, rule3 });
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>( rule3,  p3, new MockedReduceInput());
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(5, situations.Count);
			Assert.AreEqual('a', ((ITerminalInput<char>)situations[2].Input).Value);
			Assert.AreEqual('a', ((ITerminalInput<char>)situations[4].Input).Value);



		}

	}
}
