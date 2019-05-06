using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
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
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactoryContext<char>(new MockedSegmentFactoryProvider<char>(), null));
			Assert.ThrowsException<ArgumentNullException>(() => new GraphFactoryContext<char>(null,new Graph<char>()));
		}

		[TestMethod]
		public void ShouldBuildSegmentFromBasicSequence()
		{
			Segment<char> segment;
			Rule<char> rule;
			Sequence<char> predicate;
			GraphFactoryContext<char> context;

			context=new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(),new Graph<char>());

			predicate = new char[] { 'a', 'b', 'c' };
			rule = new Rule<char>();
			rule.Predicate = predicate;

			segment = context.BuildSegment( rule);
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Inputs.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
		}

		[TestMethod]
		public void ShouldReturnTargetNode()
		{
			GraphFactoryContext<char> context;
			Transition<char> transition;
			Node<char> target;
			Graph<char> graph;


			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
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
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());
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

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());
			a = context.CreateNode();
			b = context.CreateNode();

			Assert.AreEqual(0, context.GetNodeIndex(a));
			Assert.AreEqual(1, context.GetNodeIndex(b));
		}
		[TestMethod]
		public void ShouldReturnMinusOneIfNodeDoesntExists()
		{
			GraphFactoryContext<char> context;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			Assert.AreEqual(-1, context.GetNodeIndex(new Node<char>()));
		}

		[TestMethod]
		public void ShouldCreateNode()
		{
			GraphFactoryContext<char> context;
			Node<char> a, b;
			Graph<char> graph;

			graph = new Graph<char>();
			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);

			a = context.CreateNode();
			Assert.IsNotNull(a);
			Assert.AreEqual(1, graph.Nodes.Count);
			b = context.CreateNode();
			Assert.IsNotNull(b);
			Assert.AreEqual(2, graph.Nodes.Count);
		}

		[TestMethod]
		public void ShouldThrowExceptionIfParametersAreNull()
		{
			GraphFactoryContext<char> context;

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), new Graph<char>());

			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(null, Enumerable.Empty<Transition<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => context.Connect(Enumerable.Empty<Node<char>>(), null));

		}

		[TestMethod]
		public void ShouldConnectOneToOne()
		{
			Graph<char> graph;
			Node<char> a, b;
			IInput<char> input;
			Transition<char> transition;
			GraphFactoryContext<char> context;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			input = new TerminalInput<char>();
			transition = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(b) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(),graph);
			context.Connect(a.AsEnumerable(), transition.AsEnumerable());

			Assert.AreEqual(1, a.Transitions.Count);
			Assert.AreEqual(0, b.Transitions.Count);
			Assert.AreEqual(1, a.Transitions[0].TargetNodeIndex);
			Assert.AreEqual(input, a.Transitions[0].Input);
		}

		[TestMethod]
		public void ShouldAddRecognizedRules()
		{
			Graph<char> graph;
			GraphFactoryContext<char> context;
			Node<char> a;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(a.AsEnumerable(), new EORTransition<char>() { Rule = "rule" }.AsEnumerable());

			Assert.AreEqual(0, a.Transitions.Count);
			Assert.AreEqual(1, a.RecognizedRules.Count);
			Assert.AreEqual("rule", a.RecognizedRules[0]);
		}

		[TestMethod]
		public void ShouldConnectOneToMany()
		{
			Graph<char> graph;
			GraphFactoryContext<char> context;
			Node<char> a, b, c;
			Transition<char> transitionToB, transitionToC;
			IInput<char> input;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			input = new TerminalInput<char>();
			transitionToB = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(b) };
			transitionToC = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(c) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(a.AsEnumerable(), new Transition<char>[] { transitionToB, transitionToC });

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
			GraphFactoryContext<char> context;
			Node<char> a, b, c;
			IInput<char> input;
			Transition<char> transition;

			graph = new Graph<char>();
			a = new Node<char>(); graph.Nodes.Add(a);
			b = new Node<char>(); graph.Nodes.Add(b);
			c = new Node<char>(); graph.Nodes.Add(c);
			input = new TerminalInput<char>();
			transition = new Transition<char>() { Input = input, TargetNodeIndex = graph.Nodes.IndexOf(c) };

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(new Node<char>[] { a, b }, transition.AsEnumerable());

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
			GraphFactoryContext<char> context;
			Node<char> a, b, c, d;
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

			context = new GraphFactoryContext<char>(new SegmentFactoryProvider<char>(), graph);
			context.Connect(new Node<char>[] { a, b }, new Transition<char>[] { transitionToC, transitionToD });

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
