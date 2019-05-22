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
	public class OrSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OrSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  new MockedPredicate<char>(), Enumerable.Empty<ReductionTransition<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new Or<char>(), Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  null, Enumerable.Empty<ReductionTransition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(),  new Or<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OrSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			SegmentFactoryProvider<char> provider;
			Or<char> Or;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new GraphFactoryContext<char>(provider,graph);
			factory = new OrSegmentFactory<char>( provider);

			Or = new Or<char>();
			Or.Items.Add(new Terminal<char>() { Value = 'a' });
			Or.Items.Add(new Terminal<char>() { Value = 'b' });
			Or.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context,  Or, Enumerable.Empty<ReductionTransition<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.ElementAt(0)).Match('a'));
			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.ElementAt(1)).Match('b'));
			Assert.AreEqual(true, ((TerminalTransition<char>)segment.Inputs.ElementAt(2)).Match('c'));
		}



	}
}
