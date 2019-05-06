using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.SegmentFactories;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	
	[TestClass]
	public class NodeConnectorUnitTest
	{
		[TestMethod]
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			NodeConnector<char> connector;

			connector = new NodeConnector<char>();
			Assert.ThrowsException<ArgumentNullException>(() => connector.Connect( null, Enumerable.Empty<Transition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => connector.Connect( Enumerable.Empty<Node<char>>(), null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b;
			IInput<char> input;
			Transition<char> transition;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			input = new TerminalInput<char>();
			transition = new Transition<char>() { Input=input, TargetNodeIndex=graph.Nodes.IndexOf(b)};

			connector = new NodeConnector<char>();
			connector.Connect( a.AsEnumerable(), transition.AsEnumerable());

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(input, a.Transitions[0].Input);
		}

		[TestMethod]
		public void ShouldAddRecognizedRules()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);

			connector = new NodeConnector<char>();
			connector.Connect(a.AsEnumerable(), new EORTransition<char>() {Rule="rule" }.AsEnumerable());

			Assert.AreEqual(0, a.Transitions.Count);
			Assert.AreEqual(1, a.RecognizedRules.Count);
			Assert.AreEqual("rule", a.RecognizedRules[0]);
		}

		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b,c;
			Transition<char> transitionToB, transitionToC;
			IInput<char> input;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			input = new TerminalInput<char>();
			transitionToB = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(b) };
			transitionToC = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(c) };

			connector = new NodeConnector<char>();
			connector.Connect( a.AsEnumerable(), new Transition<char>[] { transitionToB,transitionToC });

			Assert.AreEqual(2, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, a.Transitions[1].TargetNodeIndex);
			Assert.AreEqual(input, a.Transitions[0].Input);
			Assert.AreEqual(input, a.Transitions[1].Input);
		}
		[TestMethod]
		public void ShouldConnectManyToOne()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b, c;
			IInput<char> input;
			Transition<char> transition;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			input = new TerminalInput<char>();
			transition = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(c) };

			connector = new NodeConnector<char>();
			connector.Connect( new Node<char>[] { a,b }, transition.AsEnumerable());

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(1, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(2, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(input, a.Transitions[0].Input);
			Assert.AreEqual(input, b.Transitions[0].Input);
		}
		[TestMethod]
		public void ShouldConnectManyToMany()
		{
			Graph<char> graph;
			NodeConnector<char> connector;
			Node<char> a, b, c,d;
			IInput<char> input;
			Transition<char> transitionToD, transitionToC;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			d = new Node<char>(); graph.Nodes.Add(d);
			input = new TerminalInput<char>();
			transitionToC = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(c) };
			transitionToD = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(d) };

			connector = new NodeConnector<char>();
			connector.Connect( new Node<char>[] { a, b }, new Transition<char>[] { transitionToC,transitionToD });

			Assert.AreEqual(2, a.Transitions.Count);
			Assert.AreEqual(2, b.Transitions.Count);
			Assert.AreEqual(0, c.Transitions.Count);
			Assert.AreEqual(0, d.Transitions.Count);
			Assert.AreEqual(2, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(2, b.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(3, a.Transitions[1].TargetNodeIndex);
			Assert.AreEqual(3, b.Transitions[1].TargetNodeIndex);

			Assert.AreEqual(input, a.Transitions[0].Input);
			Assert.AreEqual(input, b.Transitions[0].Input);
			Assert.AreEqual(input, a.Transitions[1].Input);
			Assert.AreEqual(input, b.Transitions[1].Input);
		}
	}
}
