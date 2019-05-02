using System;
using System.Linq;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class SituationProducerUnitTest
	{
		


		[TestMethod]
		public void ShouldGetDistinctInputs()
		{
			Situation<char> a;
			MockedGraph graph;
			IInput<char>[] distinctInputs;
			SituationProducer<char> producer;

			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				   -a-> O4 -b-> O5|
				   -a-> O6 -c-> O7
			*/

			graph = new MockedGraph();

			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs=producer.GetNextInputs(new Situation<char>[] { a }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
		}

		[TestMethod]
		public void ShouldGetDistinctInputs2()
		{
			Situation<char> a, b, c;
			MockedGraph graph;
			IInput<char>[] distinctInputs;
			SituationProducer<char> producer;

			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				   -a-> O4 -b-> O5|
				   -a-> O6 -c-> O7
			*/

			graph = new MockedGraph();

			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 1 };
			c = new Situation<char>() { Graph = graph, NodeIndex = 2 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextInputs(new Situation<char>[] { a, b, c }).ToArray();

			Assert.AreEqual(3, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));
			Assert.IsTrue(distinctInputs[2].Match('c'));
		}

		[TestMethod]
		public void ShouldGetNextSituations()
		{
			Situation<char> a, b;
			MockedGraph graph;
			Situation<char>[] distinctSituations;
			SituationProducer<char> producer;

			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				   -a-> O4 -b-> O5|
				   -a-> O6 -c-> O7
			*/

			graph = new MockedGraph();

			a = new Situation<char>() { Graph = graph, NodeIndex = 2 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 5 };

			producer = new SituationProducer<char>();
			distinctSituations = producer.GetNextSituations(new Situation<char>[] { a, b },new OneInput<char>() {Value='a' } ).ToArray();

			Assert.AreEqual(1, distinctSituations.Length);
			Assert.AreEqual(1,distinctSituations[0].NodeIndex);
		}
		[TestMethod]
		public void ShouldGetNextSituations2()
		{
			Situation<char> a, b;
			MockedGraph graph;
			Situation<char>[] distinctSituations;
			SituationProducer<char> producer;

			/*
					   |---- a ---|   
					   |          |   
				O0 -a-> O1 -b-> O2|-c-> O3 
				   -a-> O4 -b-> O5|
				   -a-> O6 -c-> O7
			*/

			graph = new MockedGraph();

			a = new Situation<char>() { Graph = graph, NodeIndex = 0 };
			b = new Situation<char>() { Graph = graph, NodeIndex = 5 };

			producer = new SituationProducer<char>();
			distinctSituations = producer.GetNextSituations(new Situation<char>[] { a, b }, new OneInput<char>() { Value = 'a' }).ToArray();

			Assert.AreEqual(3, distinctSituations.Length);
			Assert.AreEqual(1, distinctSituations[0].NodeIndex);
			Assert.AreEqual(4, distinctSituations[1].NodeIndex);
			Assert.AreEqual(6, distinctSituations[2].NodeIndex);
		}

	}
}
