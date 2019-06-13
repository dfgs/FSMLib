using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;
using System.Linq;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;

namespace FSMLib.LexicalAnalysis.UnitTest.Situations
{
	[TestClass]
	public class SituationGraphFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SituationGraphFactory<char>(null));
		}

		[TestMethod]
		public void ShouldNotBuildSituationGraph()
		{
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());
			Assert.ThrowsException<ArgumentNullException>(() => situationGraphFactory.BuildSituationGraph(null)) ;
		}



		[TestMethod]
		public void ShouldBuildInputPredicate()
		{
			SituationPredicate<char> predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			predicate = new Letter() { Value = 'a' };
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));

			predicate = new NonTerminal() { Name = "A" };
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));


			predicate = new AnyLetter();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));

		}
		[TestMethod]
		public void ShouldBuildAnyLetterPredicate()
		{
			SituationPredicate<char> predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());


			predicate = new AnyLetter();
			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.AreEqual(3,graph.Nodes.Count);

		}
		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };
			c = new Letter() { Value = 'c' };

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));



		}


		[TestMethod]
		public void ShouldBuildOrPredicate()
		{
			Letter a, b, c;
			Or predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };
			c = new Letter() { Value = 'c' };

			predicate = new Or();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));


		}


		[TestMethod]
		public void ShouldBuildOptionalPredicate()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };
			c = new Letter() { Value = 'c' };

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(new Optional() { Item=b  } );
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));




		}

		[TestMethod]
		public void ShouldBuildZeroOrMorePredicate()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };
			c = new Letter() { Value = 'c' };

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(new ZeroOrMore() { Item = b });
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));



		}

		[TestMethod]
		public void ShouldBuildOneOrMorePredicate()
		{
			Letter a, b, c;
			Sequence predicate;
			SituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Letter() { Value = 'a' };
			b = new Letter() { Value = 'b' };
			c = new Letter() { Value = 'c' };

			predicate = new Sequence();
			predicate.Items.Add(a);
			predicate.Items.Add(new OneOrMore() { Item = b });
			predicate.Items.Add(c);

			rule = new LexicalRule() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));



		}
		//*/



	}
}
