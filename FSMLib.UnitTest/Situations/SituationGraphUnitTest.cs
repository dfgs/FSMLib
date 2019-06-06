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
	public class SituationGraphUnitTest
	{
		

		

		[TestMethod]
		public void ShouldContains()
		{
			Terminal<char> predicate;
			SituationGraph<char> graph;
			SituationNode<char> node;
			SituationEdge<char> edge;

			predicate = new Terminal<char>() { Value = 'a' };

			node = new SituationNode<char>();
			edge = new SituationEdge<char>();
			edge.Predicate = predicate;
			node.Edges.Add(edge);

			graph = new SituationGraph<char>();
			graph.Nodes.Add(node);
			
			Assert.IsTrue(graph.Contains(predicate));
			Assert.IsFalse(graph.Contains(new AnyTerminal<char>()));
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

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			situationGraphFactory = new SituationGraphFactory<char>();
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = graph.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'b' }).ToArray();
			Assert.AreEqual(0, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = graph.CreateNextSituations(situation.AsEnumerable(),new TerminalInput<char>() {Value='a' }).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = graph.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'b' }).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			situation = new Situation<char>() { Rule = rule, Predicate = c };
			items = graph.CreateNextSituations(situation.AsEnumerable(), new TerminalInput<char>() { Value = 'c' }).ToArray();
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

			situationGraphFactory = new SituationGraphFactory<char>();
			graph = situationGraphFactory.BuildSituationGraph(new Rule<char>[] { rule1, rule2, rule3, rule4 });

			situation = new Situation<char>() { Rule = rule1, Predicate = p1 };

			situations = graph.Develop(situation.AsEnumerable());


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
