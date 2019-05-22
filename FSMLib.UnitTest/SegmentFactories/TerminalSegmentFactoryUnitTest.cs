using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class TerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new TerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			TerminalSegmentFactory<char> factory;

			factory = new TerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment(new MockedGraphFactoryContext() , new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>()));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			TerminalSegmentFactory<char> factory;

			factory = new TerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new Terminal<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  new Terminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			TerminalSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new TerminalSegmentFactory<char>( provider);

			segment=factory.BuildSegment(context,  new Terminal<char>() { Value='a' }, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.First()).Match('a'));
		}



	}
}
