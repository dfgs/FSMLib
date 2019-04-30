using System;
using System.Linq;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class SituationProducerUnitTest
	{
		
		[TestMethod]
		public void ShouldGetDistinctInputs()
		{
			Situation<char> a, b,c;
			Graph<char> graph;
			Node<char> nodeA, nodeB, nodeC;
			IInput<char>[] distinctInputs;
			SituationProducer<char> producer;

			graph = new Graph<char>();
			nodeA = graph.CreateNode();
			nodeA.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			nodeB = graph.CreateNode();
			nodeB.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			nodeC = graph.CreateNode();
			nodeC.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });

			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 1 };
			c = new Situation<char>() { Graph = graph, NodeIndex = 2 };


			producer = new SituationProducer<char>();
			distinctInputs=producer.GetDistinctInputs(new Situation<char>[] { a, b, c }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
		}

		[TestMethod]
		public void ShouldGetDistinctInputs2()
		{
			Situation<char> a, b, c;
			Graph<char> graph;
			Node<char> nodeA, nodeB, nodeC;
			IInput<char>[] distinctInputs;
			SituationProducer<char> producer;

			graph = new Graph<char>();
			nodeA = graph.CreateNode();
			nodeA.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });
			nodeB = graph.CreateNode();
			nodeB.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'b' } });
			nodeC = graph.CreateNode();
			nodeC.Transitions.Add(new Transition<char>() { TargetNodeIndex = 1, Input = new OneInput<char>() { Value = 'a' } });

			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 1 };
			c = new Situation<char>() { Graph = graph, NodeIndex = 2 };


			producer = new SituationProducer<char>();
			distinctInputs = producer.GetDistinctInputs(new Situation<char>[] { a, b, c }).ToArray();

			Assert.AreEqual(2, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));
		}


	}
}
