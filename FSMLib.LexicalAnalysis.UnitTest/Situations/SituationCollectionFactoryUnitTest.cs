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
using FSMLib.LexicalAnalysis.Situations;
using FSMLib.LexicalAnalysis.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Situations
{
	
	[TestClass]
	public class SituationCollectionFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SituationCollectionFactory(null));
		}



		[TestMethod]
		public void ShouldCreateAxiomSituations()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			Situation<char>[] items;
			LexicalRule rule1,rule2;
			SituationCollectionFactory factory;

			a = new Letter('a');
			b = new Letter('b');
			c = new Letter('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule1 = new LexicalRule() { Name = "A", Predicate = predicate, IsAxiom = true };
			rule2 = new LexicalRule() { Name = "A", Predicate = predicate, IsAxiom = false };
			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1,rule2});

			factory = new SituationCollectionFactory(graph);

			items = factory.CreateAxiomSituations().ToArray();
			Assert.AreEqual(1,items.Length);
			Assert.AreEqual(rule1, items[0].Rule);
			Assert.AreEqual(a, items[0].Predicate);
		}


		[TestMethod]
		public void ShouldCreateNextSituations()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			Situation<char>[] items;
			LexicalRule rule;
			Situation<char> situation;
			SituationCollectionFactory factory;

			a = new Letter('a');
			b = new Letter('b');
			c = new Letter('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput( 'b')).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(),new LetterInput('a')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput( 'b')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = c };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput( 'c')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Predicate.Equals(new Reduce()));
		}

		[TestMethod]
		public void ShouldCreateNextSituationsUsingAnyTerminal()
		{
			Letter a, c;
			AnyLetter b;
			Sequence predicate;
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			Situation<char>[] items;
			LexicalRule rule;
			Situation<char> situation;
			SituationCollectionFactory factory;

			a = new Letter('a');
			b = new AnyLetter();
			c = new Letter('c');

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput('b')).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput('a')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput('b')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new AnyLetterInput()).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = c };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new LetterInput('c')).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Predicate.Equals(new Reduce()));
		}


		[TestMethod]
		public void ShouldDevelopSituations()
		{
			SituationGraphFactory situationGraphFactory; 
			SituationGraph<char> graph;
			LexicalRule rule1, rule2, rule3,rule4;
			Situation<char> situation;
			ISituationCollection<char> situations;
			NonTerminal p1, p2;
			Letter p3, p4;
			Sequence sequence;
			SituationCollectionFactory factory;

			p1 = new NonTerminal() { Name = "B" };
			p2 = new NonTerminal() { Name = "C" };
			p3 = new Letter('b');
			p4 = new Letter('c');

			sequence = new Sequence();
			sequence.Items.Add(p1);
			sequence.Items.Add(new Letter('d'));

			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };
			rule2 = new LexicalRule() { Name = "B", Predicate = p2 };
			rule3 = new LexicalRule() { Name = "B", Predicate = p3 };
			rule4 = new LexicalRule() { Name = "C", Predicate = p4 };

			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2, rule3, rule4 });
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule1, Predicate = p1 };

			situations = factory.Develop(situation.AsEnumerable());


			Assert.AreEqual(4, situations.Count);
			Assert.AreEqual(p1, situations[0].Predicate);
			Assert.AreEqual(p2, situations[1].Predicate);
			Assert.IsTrue(situations[1].Input.Match('d'));
			Assert.AreEqual(p3, situations[2].Predicate);
			Assert.IsTrue(situations[2].Input.Match('d'));
			Assert.AreEqual(p4, situations[3].Predicate);
			Assert.IsTrue(situations[3].Input.Match('d'));

		}

		[TestMethod]
		public void ShouldDevelopSituationEndingWithReductionOnNonTerminal()
		{
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			LexicalRule rule1, rule2;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Letter p1;
			NonTerminal p2;
			Letter  p3;
			Sequence sequence;
			SituationCollectionFactory factory;

			p1 = new Letter('a');
			p2 = new NonTerminal() { Name = "B" };
			p3 = new Letter('b');

			sequence = new Sequence();
			sequence.Items.Add(p1);
			sequence.Items.Add(p2);

			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };
			rule2 = new LexicalRule() { Name = "B", Predicate = p3 };

			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2 });
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule1, Predicate = p2 };
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(2, situations.Count);
			Assert.AreEqual(p3, situations[1].Predicate);


		}

		[TestMethod]
		public void ShouldDevelopSituationWithLeftRecursiveReduction()
		{
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			LexicalRule rule1, rule2,rule3;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Letter p1;
			NonTerminal p2;
			Letter p3;
			Letter p4;
			Sequence sequence;
			SituationCollectionFactory factory;

			//A*=•{S}a
			//S=•{S}b 
			//S=•c

			p1 = new Letter('a');
			p2 = new NonTerminal() { Name = "S" };
			p3 = new Letter('b');
			p4 = new Letter('c');

			sequence = new Sequence();
			sequence.Items.Add(p2);
			sequence.Items.Add(p1);
			rule1 = new LexicalRule() { Name = "A", Predicate = sequence };

			sequence = new Sequence();
			sequence.Items.Add(p2);
			sequence.Items.Add(p3);
			rule2 = new LexicalRule() { Name = "S", Predicate = sequence };

			rule3 = new LexicalRule() { Name = "S", Predicate = p4 };

			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2,rule3 });
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule1, Predicate = p2 };
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(5, situations.Count);
			Assert.IsTrue(situations[1].Input.Match('a'));
			Assert.IsTrue(situations[2].Input.Match('b'));
			Assert.IsTrue(situations[3].Input.Match('a'));
			Assert.IsTrue(situations[4].Input.Match('b'));



		}

		[TestMethod]
		public void ShouldDevelopSituationWithLoopReduction()
		{
			SituationGraphFactory situationGraphFactory;
			SituationGraph<char> graph;
			LexicalRule rule1, rule2, rule3;
			Situation<char> situation;
			ISituationCollection<char> situations;
			Letter p1;
			NonTerminal p2;
			NonTerminal p3;
			SituationCollectionFactory factory;
			OneOrMore oneOrMore;

			//"L=a"
			//"N={L}+"
			//"A*={N}"

			p1 = new Letter('a');
			p2 = new NonTerminal() { Name = "L" };
			p3 = new NonTerminal() { Name = "N" };

			rule1 = new LexicalRule() { Name = "L", Predicate = p1 };

			oneOrMore = new OneOrMore();
			oneOrMore.Item = p2;
			rule2 = new LexicalRule() { Name = "N", Predicate = oneOrMore};

			rule3 = new LexicalRule() { Name = "A", Predicate = p3 };

			situationGraphFactory = new SituationGraphFactory(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new LexicalRule[] { rule1, rule2, rule3 });
			factory = new SituationCollectionFactory(graph);

			situation = new Situation<char>() { Rule = rule3, Predicate = p3 };
			situations = factory.Develop(situation.AsEnumerable());

			Assert.AreEqual(5, situations.Count);
			Assert.IsTrue(situations[2].Input.Match('a'));
			Assert.IsTrue(situations[4].Input.Match('a'));



		}

	}
}
