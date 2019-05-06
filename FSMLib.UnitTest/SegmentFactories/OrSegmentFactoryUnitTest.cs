﻿using FSMLib.Graphs;
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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), new MockedNodeConnector(), new MockedPredicate<char>(), new EORTransition<char>().AsEnumerable()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeConnector(), new Or<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), null, new Or<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), new MockedNodeConnector(), null, new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedGraphFactoryContext(), new MockedNodeConnector(), new Or<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OrSegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;
			Or<char> Or;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(graph);
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new OrSegmentFactory<char>( provider);

			Or = new Or<char>();
			Or.Items.Add(new Terminal<char>() { Value = 'a' });
			Or.Items.Add(new Terminal<char>() { Value = 'b' });
			Or.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context, connector, Or, new EORTransition<char>().AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Inputs.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, graph.Nodes.Count);
			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.ElementAt(0)).Input.Match('a'));
			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.ElementAt(1)).Input.Match('b'));
			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.ElementAt(2)).Input.Match('c'));
		}



	}
}
