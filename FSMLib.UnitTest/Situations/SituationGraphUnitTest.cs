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
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SituationGraph<char>(null));
		}

		[TestMethod]
		public void ShouldContains()
		{
			Terminal<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;

			predicate = new Terminal<char>() { Value = 'a' };
			rule = new Rule<char>() { Name = "A",Predicate=predicate};
			graph = new SituationGraph<char>(rule.AsEnumerable());

			Assert.IsTrue(graph.Contains(predicate));
			Assert.IsFalse(graph.Contains(new AnyTerminal<char>()));
		}

		[TestMethod]
		public void ShouldBuildInputPredicate()
		{
			InputPredicate<char> predicate;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;

			predicate = new Terminal<char>() { Value = 'a' };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate};
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			//Assert.IsTrue(graph.CanReduce(predicate));

			predicate = new NonTerminal<char>() { Name = "A" };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate };
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			//Assert.IsTrue(graph.CanReduce(predicate));

			predicate = new AnyTerminal<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate };
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			//Assert.IsTrue(graph.CanReduce(predicate));
		}

		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
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
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };

			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(1, items.Length);

			//Assert.IsFalse(graph.CanReduce(a));
			//Assert.IsFalse(graph.CanReduce(b));
			//Assert.IsTrue(graph.CanReduce(c));

		}


		[TestMethod]
		public void ShouldBuildOrPredicate()
		{
			Terminal<char> a, b, c;
			Or<char> predicate;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Or<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = b };
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);

			situation = new Situation<char>() { Rule = rule, Predicate = c };
			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);

			//Assert.IsTrue(graph.CanReduce(a));
			//Assert.IsTrue(graph.CanReduce(b));
			//Assert.IsTrue(graph.CanReduce(c));
		}


		[TestMethod]
		public void ShouldBuildOptionalPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new Optional<char>() { Item=b  } );
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };

			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(b, items[0].Predicate);
			Assert.AreEqual(c,items[1].Predicate);

			/*predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new Optional<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());*/

			//Assert.IsFalse(graph.CanReduce(a));
			//Assert.IsTrue(graph.CanReduce(b));
			//Assert.IsTrue(graph.CanReduce(c));

		}

		[TestMethod]
		public void ShouldBuildZeroOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new ZeroOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };

			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(b, items[0].Predicate);
			Assert.AreEqual(c, items[1].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(c, items[0].Predicate);
			Assert.AreEqual(b, items[1].Predicate);


			/*predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new ZeroOrMore<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());*/

			//Assert.IsFalse(graph.CanReduce(a));
			//Assert.IsTrue(graph.CanReduce(b));
			//Assert.IsTrue(graph.CanReduce(c));
		}

		[TestMethod]
		public void ShouldBuildOneOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Situation<char>[] items;
			Rule<char> rule;
			Situation<char> situation;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new OneOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };

			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b,items[0].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(c, items[0].Predicate);
			Assert.AreEqual(b, items[1].Predicate);


			/*predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new OneOrMore<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());*/

			//Assert.IsFalse(graph.CanReduce(a));
			//Assert.IsFalse(graph.CanReduce(b));
			//Assert.IsTrue(graph.CanReduce(c));
		}

		[TestMethod]
		public void ShouldGetNextPredicates()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
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
			graph = new SituationGraph<char>(rule.AsEnumerable());

			situation = new Situation<char>() { Rule = rule, Predicate = a };

			items = graph.GetNextSituations(situation).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0].Predicate);

			items = graph.GetNextSituations(items[0]).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(ReducePredicate<char>.Instance, items[0].Predicate);
		}

		


		[TestMethod]
		public void ShouldDevelopSituations()
		{
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

			graph = new SituationGraph<char>(new Rule<char>[] { rule1, rule2, rule3,rule4 });

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

		/*
		[TestMethod]
		public void ShouldGetInputsAfterPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			Or<char> or;
			SituationGraph<char> graph;
			Rule<char> rule;
			BaseInput<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };
			or = new Or<char>();
			or.Items.Add(b);
			or.Items.Add(c);

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(or);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			items = graph.GetTerminalInputsAfterPredicate(a).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items[0].Match( new TerminalInput<char>() { Value = 'b' }));
			Assert.IsTrue(items[1].Match(new TerminalInput<char>() { Value = 'c' }));

			items = graph.GetTerminalInputsAfterPredicate(b).ToArray();
			Assert.AreEqual(1, items.Length);


		}

		[TestMethod]
		public void ShouldGetDistinctsInputsAfterPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			Or<char> or;
			SituationGraph<char> graph;
			Rule<char> rule;
			BaseInput<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'b' };
			or = new Or<char>();
			or.Items.Add(b);
			or.Items.Add(c);

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(or);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());

			items = graph.GetTerminalInputsAfterPredicate(a).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Match(new TerminalInput<char>() { Value = 'b' }));

			items = graph.GetTerminalInputsAfterPredicate(b).ToArray();
			Assert.AreEqual(1, items.Length);

	
		}

		[TestMethod]
		public void ShouldGetDistinctsInputsAfterPredicate2()
		{
			Terminal<char> a, c;
			Sequence<char> predicate;
			NonTerminal<char> b;
			SituationGraph<char> graph;
			Rule<char> rule1,rule2,rule3;
			BaseInput<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new NonTerminal<char>() { Name="B" };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule1 = new Rule<char>() { Name = "A", Predicate = predicate };
			rule2 = new Rule<char>() { Name = "B", Predicate = new NonTerminal<char>() { Name = "C" } };
			rule3 = new Rule<char>() { Name = "C", Predicate = new Terminal<char>() { Value = 'b' } };

			graph = new SituationGraph<char>(new Rule<char>[] { rule1,rule2,rule3} );

			items = graph.GetTerminalInputsAfterPredicate(a).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.IsTrue(items[0].Match(new NonTerminalInput<char>() { Name= "B" }));



		}*/


	}
}
