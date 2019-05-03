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
	public class AnySegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AnySegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			AnySegmentFactory<char> factory;

			factory = new AnySegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment( new MockedNodeContainer() ,new MockedNodeConnector(), new MockedPredicate<char>(),new EORTransition<char>().AsEnumerable() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			AnySegmentFactory<char> factory;

			factory = new AnySegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new MockedNodeConnector(), new Any<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), null, new Any<char>(), new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), new MockedNodeConnector(), null, new EORTransition<char>().AsEnumerable()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedNodeContainer(), new MockedNodeConnector(), new Any<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			AnySegmentFactory<char> factory;
			Segment<char> segment;
			Graph<char> graph;
			NodeConnector<char> connector;
			SegmentFactoryProvider<char> provider;

			graph = new Graph<char>();
			connector = new NodeConnector<char>();
			provider = new SegmentFactoryProvider<char>();
			factory = new AnySegmentFactory<char>( provider);

			segment=factory.BuildSegment( graph, connector, new Any<char>(), new EORTransition<char>().AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
			Assert.AreEqual(true, ((Transition<char>)segment.Inputs.First()).Input.Match('a'));
		}



	}
}
