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
	public class GraphFactoryContextUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactoryContext<char>(null));
		}

		[TestMethod]
		public void ShouldReturnTargetNode()
		{
			GraphFactoryContext<char> context;
			Transition<char> transition;
			Node<char> target;
			Graph<char> graph;


			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(graph);
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			transition = new Transition<char>() { TargetNodeIndex = 1 };
			graph.Nodes[0].Transitions.Add(transition);

			target = context.GetTargetNode(transition);
			Assert.AreEqual(graph.Nodes[1], target);
		}

		[TestMethod]
		public void ShouldReturnNotTargetNode()
		{
			GraphFactoryContext<char> context;
			Graph<char> graph;
			Transition<char> transition;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(graph);
			graph.Nodes.Add(new Node<char>());
			graph.Nodes.Add(new Node<char>());

			transition = new Transition<char>() { TargetNodeIndex = 2 };  // Index out of range
			graph.Nodes[0].Transitions.Add(transition);

			Assert.ThrowsException<IndexOutOfRangeException>(() => context.GetTargetNode(transition));
		}
		[TestMethod]
		public void ShouldReturnNodeIndex()
		{
			GraphFactoryContext<char> context;
			Node<char> a, b;

			context = new GraphFactoryContext<char>(new Graph<char>());
			a = context.CreateNode();
			b = context.CreateNode();

			Assert.AreEqual(0, context.GetNodeIndex(a));
			Assert.AreEqual(1, context.GetNodeIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfNodeDoesntExists()
		{
			GraphFactoryContext<char> context;

			context = new GraphFactoryContext<char>(new Graph<char>());

			Assert.AreEqual(-1, context.GetNodeIndex(new Node<char>()));
		}

		[TestMethod]
		public void ShouldCreateNode()
		{
			GraphFactoryContext<char> context;
			Node<char> a, b;
			Graph<char> graph;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(graph);

			a = context.CreateNode();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, graph.Nodes.Count);
			b = context.CreateNode();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, graph.Nodes.Count);
		}

	}

}
