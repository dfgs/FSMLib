using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;

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


			predicate = new Terminal<char>() { Value = 'a' };
			graph = new SituationGraph<char>(predicate.AsEnumerable());

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

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
		

			items = graph.GetRootInputPredicates(predicate).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(a, items[0]);


			a = new Optional<char>() { Item = new Terminal<char>() { Value = 'a' } };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());


			items = graph.GetRootInputPredicates(predicate).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual((a as Optional<char>).Item, items[0]);
			Assert.AreEqual(b, items[1]);

		}

		[TestMethod]
		public void ShouldBuildInputPredicate()
		{
			InputPredicate<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			predicate = new Terminal<char>() { Value = 'a' };
			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(0, items.Length);

			predicate = new NonTerminal<char>() { Name = "A" };
			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(0, items.Length);

			predicate = new AnyTerminal<char>();
			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			items = graph.GetNextPredicates(predicate).ToArray();
			Assert.AreEqual(0, items.Length);
		}

		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
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
			Assert.AreEqual(0, items.Length);
		}


		[TestMethod]
		public void ShouldBuildOrPredicate()
		{
			Terminal<char> a, b, c;
			Or<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Or<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(0, items.Length);

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(0, items.Length);

			items = graph.GetNextPredicates(c).ToArray();
			Assert.AreEqual(0, items.Length);
		}


		[TestMethod]
		public void ShouldBuildOptionalPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new Optional<char>() { Item=b  } );
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));
			Assert.IsTrue(items.Contains(c));
		
		}

		[TestMethod]
		public void ShouldBuildZeroOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new ZeroOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
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
		}

		[TestMethod]
		public void ShouldBuildOneOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			InputPredicate<char>[] items;

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new OneOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			graph = new SituationGraph<char>(predicate.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			items = graph.GetNextPredicates(a).ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b,items[0]);

			items = graph.GetNextPredicates(b).ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.IsTrue(items.Contains(b));
		}


	}
}
