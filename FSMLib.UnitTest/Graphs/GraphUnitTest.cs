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
			Graph<char> graph;
			Transition<char> transition;
			Node<char> target;

			graph = new Graph<char>();
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			transition = new Transition<char>() { TargetNodeIndex = 1 };
			graph.Nodes[0].Transitions.Add(transition);

			target = graph.GetTargetNode(transition);
			Assert.AreEqual(graph.Nodes[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetNode()
		{
			Graph<char> graph;
			Transition<char> transition;

			graph = new Graph<char>();
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			transition = new Transition<char>() { TargetNodeIndex = 2 };  // Index out of range
			graph.Nodes[0].Transitions.Add(transition);

			Assert.ThrowsException<IndexOutOfRangeException>(() => graph.GetTargetNode(transition));
		}
		[TestMethod]
		public void ShouldReturnNodeIndex()
		{
			Graph<char> graph;
			Node<char> a, b;

			graph = new Graph<char>();
			a = graph.CreateNode();
			b = graph.CreateNode();

			Assert.AreEqual(0, graph.GetNodeIndex(a));
			Assert.AreEqual(1, graph.GetNodeIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfNodeDoesntExists()
		{
			Graph<char> graph;

			graph = new Graph<char>();

			Assert.AreEqual(-1, graph.GetNodeIndex(new Node<char>()));
		}

		[TestMethod]
		public void ShouldCreateNode()
		{
			Graph<char> graph;
			Node<char> a, b;

			graph = new Graph<char>();
			a = graph.CreateNode();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, graph.Nodes.Count);
			b = graph.CreateNode();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, graph.Nodes.Count);
		}

	}

}
