using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class NonTerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new NonTerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			NonTerminalSegmentFactory<char> factory;

			factory = new NonTerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment(new MockedGraphFactoryContext() ,new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			NonTerminalSegmentFactory<char> factory;

			factory = new NonTerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new NonTerminal<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  new NonTerminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			NonTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			GraphFactoryContext<char> context;
			Rule<char> rule;

			rule = new Rule<char>() { Name = "S", Predicate = new Terminal<char>() { Value = 'a' } };
			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider, graph) ;
			factory = new NonTerminalSegmentFactory<char>(provider);

			segment = factory.BuildSegment(context,  new NonTerminal<char>() { Name = "S" }, Enumerable.Empty<ReductionTransition<char>>());

			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());	// not two, because translation from non terminal input is done at graph factory level
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			//Assert.AreEqual(true, ((Transition<char>)segment.Inputs.ElementAt(1)).Input.Match('a'));
			Assert.AreEqual(true, ((NonTerminalTransition<char>)segment.Inputs.ElementAt(0)).Match("S"));
		}

		[TestMethod]
		public void ShouldNotFailIfNonTerminalRuleDoesntExistsInContext()
		{
			NonTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			GraphFactoryContext<char> context;
			Rule<char> rule;

			rule = new Rule<char>() { Name = "S", Predicate = new Terminal<char>() { Value = 'a' } };
			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider, graph);
			factory = new NonTerminalSegmentFactory<char>(provider);

			segment = factory.BuildSegment(context, new NonTerminal<char>() { Name = "A" }, Enumerable.Empty<ReductionTransition<char>>());

			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			Assert.AreEqual(true, ((NonTerminalTransition<char>)segment.Inputs.ElementAt(0)).Match( "A" ));
		}

	}
}
