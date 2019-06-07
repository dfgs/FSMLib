using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;

namespace FSMLib.UnitTest.Situations
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
			Assert.ThrowsException<ArgumentNullException>(() => situationGraphFactory.BuildSituationGraph(null));
		}

		

		[TestMethod]
		public void ShouldBuildInputPredicate()
		{
			SituationPredicate<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			Situation<char> situation;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			predicate = new Terminal<char>() { Value = 'a' };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate};

			predicate = new NonTerminal<char>() { Name = "A" };
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate };


			predicate = new AnyTerminal<char>();
			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(predicate));
			situation = new Situation<char>() { Rule = rule, Predicate = predicate };

		}

		[TestMethod]
		public void ShouldBuildSequencePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			Situation<char> situation;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };


		}


		[TestMethod]
		public void ShouldBuildOrPredicate()
		{
			Terminal<char> a, b, c;
			Or<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Or<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(b);
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));


		}


		[TestMethod]
		public void ShouldBuildOptionalPredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			Situation<char> situation;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new Optional<char>() { Item=b  } );
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };



		}

		[TestMethod]
		public void ShouldBuildZeroOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			Situation<char> situation;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new ZeroOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };


		}

		[TestMethod]
		public void ShouldBuildOneOrMorePredicate()
		{
			Terminal<char> a, b, c;
			Sequence<char> predicate;
			SituationGraph<char> graph;
			Rule<char> rule;
			Situation<char> situation;
			SituationGraphFactory<char> situationGraphFactory;

			situationGraphFactory = new SituationGraphFactory<char>(new SituationGraphSegmentFactory<char>());

			a = new Terminal<char>() { Value = 'a' };
			b = new Terminal<char>() { Value = 'b' };
			c = new Terminal<char>() { Value = 'c' };

			predicate = new Sequence<char>();
			predicate.Items.Add(a);
			predicate.Items.Add(new OneOrMore<char>() { Item = b });
			predicate.Items.Add(c);

			rule = new Rule<char>() { Name = "A", Predicate = predicate };
			graph = situationGraphFactory.BuildSituationGraph(rule.AsEnumerable());
			Assert.IsTrue(graph.Contains(a));
			Assert.IsTrue(graph.Contains(b));
			Assert.IsTrue(graph.Contains(c));

			situation = new Situation<char>() { Rule = rule, Predicate = a };


		}
		//*/



	}
}
