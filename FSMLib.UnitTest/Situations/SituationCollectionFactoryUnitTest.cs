using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Inputs;

namespace FSMLib.UnitTest.Situations
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
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraphFactory<char> situationGraphFactory;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule1,rule2;
			SituationCollectionFactory<char> factory;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule1 = new Rule<char>() { Name = "A", Predicate = predicate, IsAxiom = true };
			rule2 = new Rule<char>() { Name = "A", Predicate = predicate, IsAxiom = false };
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new Rule<char>[] { rule1,rule2}, Enumerable.Empty<char>());

			factory = new SituationCollectionFactory<char>(graph);

			items = factory.CreateAxiomSituations().ToArray();
			Assert.AreEqual(1,items.Length);
			Assert.AreEqual(rule1, items[0].Rule);
			Assert.AreEqual(a, items[0].Predicate);
		}


		[TestMethod]
		public void ShouldCreateNextSituations()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraphFactory<char> situationGraphFactory;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;
			SituationCollectionFactory<char> factory;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable(), Enumerable.Empty<char>());
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'b' }).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = factory.CreateNextSituations(situation.AsEnumerable(),new TerminalInput<char>() {Value='a' }).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'b' }).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = c };
			items = factory.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'c' }).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(ReducePredicate<char>.Instance, items[0].Predicate);
		}

		


		[TestMethod]
		public void ShouldDevelopSituations()
		{
			SituationGraphFactory<char> situationGraphFactory; 
			SituationGraph<char> graph;
			Rule<char> rule1, rule2, rule3,rule4;
			Situation<char> situation;
			ISituationCollection<char> situations;
			NonTerminal<char> p1, p2;
			Terminal<char> p3, p4;
			Sequence<char> sequence;
			SituationCollectionFactory<char> factory;

			p1 = new NonTerminal<char>() { Name = "B" };
			p2 = new NonTerminal<char>() { Name = "C" };
			p3 = new Terminal<char>() { Value = 'b' };
			p4 = new Terminal<char>() { Value = 'c' };

			sequence = new Sequence<char>();
			sequence.Items.Add(p1);
			sequence.Items.Add(new Terminal<char>() { Value = 'd' });

			rule1 = new Rule<char>() { Name = "A", Predicate = sequence };
			rule2 = new Rule<char>() { Name = "B", Predicate = p2 };
			rule3 = new Rule<char>() { Name = "B", Predicate = p3 };
			rule4 = new Rule<char>() { Name = "C", Predicate = p4 };

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			graph = situationGraphFactory.BuildSituationGraph(new Rule<char>[] { rule1, rule2, rule3, rule4 }, Enumerable.Empty<char>());
			factory = new SituationCollectionFactory<char>(graph);

			situation = new Situation<char>() { Rule = rule1, Predicate = p1 };

			situations = factory.Develop(situation.AsEnumerable());


			Assert.AreEqual(4, situations.Count);
			Assert.AreEqual(p1, situations[0].Predicate);
			Assert.AreEqual(p3, situations[1].Predicate);
			Assert.IsTrue(situations[1].Input.Match('d'));
			Assert.AreEqual(p2, situations[2].Predicate);
			Assert.IsTrue(situations[2].Input.Match('d'));
			Assert.AreEqual(p4, situations[3].Predicate);
			Assert.IsTrue(situations[3].Input.Match('d'));

		}


	}
}
