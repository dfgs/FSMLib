using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Predicates;
using FSMLib.Situations;
using System.Linq;
using FSMLib.Common.Situations;
using FSMLib.Common;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.LexicalAnalysis.Predicates;

namespace FSMLib.Common.UnitTest.Situations
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
			ISituationPredicate<char> predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			predicate = new Terminal('a');
			rule = new LexicalRule() { Name = "A", Predicate = (LexicalPredicate)predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));

			predicate = new NonTerminal( "A" );
			rule = new LexicalRule() { Name = "A", Predicate = (LexicalPredicate)predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));


			predicate = new AnyTerminal();
			rule = new LexicalRule() { Name = "A", Predicate = (LexicalPredicate)predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));

		}
		[TestMethod]
		public void ShouldBuildAnyLetterPredicate()
		{
			LexicalPredicate predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());


			predicate = new AnyTerminal();
			rule = new LexicalRule() { Name = "A", Predicate =new Sequence( predicate ,new Reduce()) };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.AreEqual(3,graph.Nodes.Count());

		}
		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Terminal a, b, c;
			Sequence predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

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
			Terminal a, b, c;
			Or predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

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
			Terminal a, b, c;
			Sequence predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

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
			Terminal a, b, c;
			Sequence predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

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
			Terminal a, b, c;
			Sequence predicate;
			ISituationGraph<char> graph;
			LexicalRule rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal('a');
			b = new Terminal('b');
			c = new Terminal('c');

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
