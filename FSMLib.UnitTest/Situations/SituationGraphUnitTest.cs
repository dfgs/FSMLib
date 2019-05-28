using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;

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
		public void GetRootInputPredicates()
		{
			BasePredicate<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());


			items = graph.GetRootInputPredicates(predicate).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(a, items[0]);
			//Assert.AreEqual(null, graph.GetReduction(a as InputPredicate<char>));
			//Assert.AreEqual(null, graph.GetReduction(b as InputPredicate<char>));
			//Assert.AreEqual("A", graph.GetReduction(c as InputPredicate<char>));

			a = new Optional<char>() { Item = new Terminal<char>() { Value = 'a' } };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());


			items = graph.GetRootInputPredicates(predicate).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual((a as Optional<char>).Item, items[0]);
			Assert.AreEqual(b, items[1]);
			//Assert.AreEqual(null, graph.GetReduction((a as Optional<char>).Item as InputPredicate<char>));
			//Assert.AreEqual(null, graph.GetReduction(b as InputPredicate<char>));
			//Assert.AreEqual("A", graph.GetReduction(c as InputPredicate<char>));



		}

		[TestMethod]
		public void ShouldBuildInputPredicate()
		{
			InputPredicate<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

			predicate = new Terminal<char>() { Value = 'a' };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(1, items.Length);

			predicate = new NonTerminal<char>() { Name = "A" };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(1, items.Length);

			predicate = new AnyTerminal<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(1, items.Length);
		}

		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

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

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0]);

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(c, items[0]);

			items = graph.GetNextPredicates(c).ToArray();
			Assert.AreEqual(1, items.Length);

			//Assert.AreEqual(null, graph.GetReduction(a));
			//Assert.AreEqual(null, graph.GetReduction(b));
			//Assert.AreEqual("A", graph.GetReduction(c));
		}


		[TestMethod]
		public void ShouldBuildOrPredicate()
		{
			Terminal<char> a, b, c;
			Or<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

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

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(1, items.Length);

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(1, items.Length);

			items = graph.GetNextPredicates(c).ToArray();
			Assert.AreEqual(1, items.Length);

			//Assert.AreEqual("A", graph.GetReduction(a));
			//Assert.AreEqual("A", graph.GetReduction(b));
			//Assert.AreEqual("A", graph.GetReduction(c));
		}


		[TestMethod]
		public void ShouldBuildOptionalPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

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

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));
			Assert.IsTrue(items.Contains(c));

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new Optional<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			//Assert.AreEqual(null, graph.GetReduction(a));
			//Assert.AreEqual("A", graph.GetReduction(b));
			//Assert.AreEqual("A", graph.GetReduction(c));

		}

		[TestMethod]
		public void ShouldBuildZeroOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

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

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));
			Assert.IsTrue(items.Contains(c));

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));


			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new ZeroOrMore<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			//Assert.AreEqual(null, graph.GetReduction(a));
			//Assert.AreEqual("A", graph.GetReduction(b));
			//Assert.AreEqual("A", graph.GetReduction(c));
		}

		[TestMethod]
		public void ShouldBuildOneOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;
			Rule<char> rule;

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

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b,items[0]);

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(new OneOrMore<char>() { Item = c });

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = new SituationGraph<char>(rule.AsEnumerable());
			//Assert.AreEqual(null, graph.GetReduction(a));
			//Assert.AreEqual(null, graph.GetReduction(b));
			//Assert.AreEqual("A", graph.GetReduction(c));
		}


	}
}
