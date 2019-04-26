using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.SegmentFactories;
using FSMLib.Graphs;

namespace FSMLib.UnitTest.SegmentFactories
{
	
	[TestClass]
	public class NodeConnectorUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new NodeConnector(null));
		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			Graph graph;
			NodeConnector connector;
			Node a, b;

			graph = new Graph();
			a = graph.CreateNode();
			b = graph.CreateNode();

			connector = new NodeConnector(graph);
			connector.Connect(new Node[] { a }, new Node[] { b });

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			Graph graph;
			NodeConnector connector;
			Node a, b,c;

			graph = new Graph();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();

			connector = new NodeConnector(graph);
			connector.Connect(new Node[] { a }, new Node[] { b,c });

			Assert.AreEqual(2, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, a.Transitions[1].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			Graph graph;
			NodeConnector connector;
			Node a, b, c;

			graph = new Graph();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();

			connector = new NodeConnector(graph);
			connector.Connect(new Node[] { a,b }, new Node[] {  c });

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(1, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(2, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.Transitions[0].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			Graph graph;
			NodeConnector connector;
			Node a, b, c,d;

			graph = new Graph();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();
			d = graph.CreateNode();

			connector = new NodeConnector(graph);
			connector.Connect(new Node[] { a, b }, new Node[] { c,d });

			Assert.AreEqual(2, a.Transitions.Count);
			Assert.AreEqual(2, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(0, d.Transitions.Count);
			Assert.AreEqual(2, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(3, a.Transitions[1].TargetNodeIndex);
			Assert.AreEqual(3, b.Transitions[1].TargetNodeIndex);
		}
	}
}
