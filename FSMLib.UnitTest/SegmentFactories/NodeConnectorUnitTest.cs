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
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();

			connector = new NodeConnector<char>();
			Assert.ThrowsException<ArgumentNullException>(() => connector.Connect(null, new Node<char>[] { a }, new Node<char>[] { b }));
			Assert.ThrowsException<ArgumentNullException>(() => connector.Connect(graph, null, new Node<char>[] { b }));
			Assert.ThrowsException<ArgumentNullException>(() => connector.Connect(graph, new Node<char>[] { a }, null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();

			connector = new NodeConnector<char>();
			connector.Connect(graph,new Node<char>[] { a }, new Node<char>[] { b });

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b,c;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();

			connector = new NodeConnector<char>();
			connector.Connect(graph,new Node<char>[] { a }, new Node<char>[] { b,c });

			Assert.AreEqual(2, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, a.Transitions[1].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b, c;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();

			connector = new NodeConnector<char>();
			connector.Connect(graph,new Node<char>[] { a,b }, new Node<char>[] {  c });

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(1, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(2, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.Transitions[0].TargetNodeIndex);
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b, c,d;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();
			c = graph.CreateNode();
			d = graph.CreateNode();

			connector = new NodeConnector<char>();
			connector.Connect(graph,new Node<char>[] { a, b }, new Node<char>[] { c,d });

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
