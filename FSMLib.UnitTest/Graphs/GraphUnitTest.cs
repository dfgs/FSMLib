using FSMLib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Graphs
{
	[TestClass]
	public class GraphUnitTest
	{
		[TestMethod]
		public void ShouldReturnTargetNode()
		{
			Graph graph;
			Transition transition;
			Node target;

			graph = new Graph();
			graph.Nodes.Add(new Node());
			graph.Nodes.Add(new Node());

			transition = new Transition() { TargetNodeIndex = 1 };
			graph.Nodes[0].Transitions.Add(transition);

			target = graph.GetTargetNode(transition);
			Assert.AreEqual(graph.Nodes[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetNode()
		{
			Graph graph;
			Transition transition;

			graph = new Graph();
			graph.Nodes.Add(new Node());
			graph.Nodes.Add(new Node());

			transition = new Transition() { TargetNodeIndex = 2 };  // Index out of range
			graph.Nodes[0].Transitions.Add(transition);

			Assert.ThrowsException<IndexOutOfRangeException>(() => graph.GetTargetNode(transition));
		}
		[TestMethod]
		public void ShouldReturnNodeIndex()
		{
			Graph graph;
			Node a, b;

			graph = new Graph();
			a = graph.CreateNode();
			b = graph.CreateNode();

			Assert.AreEqual(0, graph.GetNodeIndex(a));
			Assert.AreEqual(1, graph.GetNodeIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfNodeDoesntExists()
		{
			Graph graph;

			graph = new Graph();

			Assert.AreEqual(-1, graph.GetNodeIndex(new Node()));
		}

		[TestMethod]
		public void ShouldCreateNode()
		{
			Graph graph;
			Node a, b;

			graph = new Graph();
			a = graph.CreateNode();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, graph.Nodes.Count);
			b = graph.CreateNode();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, graph.Nodes.Count);
		}

	}

}
