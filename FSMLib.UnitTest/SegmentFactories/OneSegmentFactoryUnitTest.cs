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
	public class OneSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OneSegmentFactory<char>(null, new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new OneSegmentFactory<char>(new MockedNodeContainer(), null, new MockedSegmentFactoryProvider<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new OneSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OneSegmentFactory<char> factory;

			factory = new OneSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment(new MockedPredicate<char>()));
		}
		[TestMethod]
		public void ShouldFailWithNullPredicate()
		{
			OneSegmentFactory<char> factory;

			factory = new OneSegmentFactory<char>(new MockedNodeContainer(), new MockedNodeConnector(), new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OneSegmentFactory<char> factory;
			Segment segment;
			Graph graph;
			NodeConnector connector;
			SegmentFactoryProvider<char> provider;

			graph = new Graph();
			connector = new NodeConnector(graph);
			provider = new SegmentFactoryProvider<char>(graph, connector);
			factory = new OneSegmentFactory<char>(graph,connector, provider);

			segment=factory.BuildSegment(new One<char>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, graph.Nodes.Count);
		}



	}
}
