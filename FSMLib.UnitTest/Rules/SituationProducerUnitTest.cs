using System;
using System.Linq;
using FSMLib.ActionTables;
using FSMLib.Helpers;
using FSMLib.Rules;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest
{
	[TestClass]
	public class SituationProducerUnitTest
	{
		


		[TestMethod]
		public void ShouldGetOneDistinctTerminal()
		{
			Situation<char> s1,s2,s3;
			SituationProducer<char> producer;
			char[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable('a','b','c');

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs=producer.GetNextTerminals(new Situation<char>[] { s1,s2,s3 }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.AreEqual('a', distinctInputs[0]);
		}
		[TestMethod]
		public void ShouldGetTwoDistinctTerminals()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			char[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable('a', 'b', 'c');

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 1 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminals(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(2, distinctInputs.Length);
			Assert.AreEqual('a', distinctInputs[0]);
			Assert.AreEqual('b', distinctInputs[1]);
		}

		[TestMethod]
		public void ShouldGetThreeDistinctTerminals()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			char[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable('a', 'b', 'c');

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 1 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 2 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminals(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(3, distinctInputs.Length);
			Assert.AreEqual('a',distinctInputs[0]);
			Assert.AreEqual('b', distinctInputs[1]);
			Assert.AreEqual('c', distinctInputs[2]);
		}





		[TestMethod]
		public void ShouldGetOneDistinctNonTerminal()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable("A", "B", "C");

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			
		}
		[TestMethod]
		public void ShouldGetTwoDistinctNonTerminal()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable("A", "B", "C");

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 1 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(2, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			Assert.AreEqual("B", distinctInputs[1]);
		}
		[TestMethod]
		public void ShouldGetThreeDistinctNonTerminal()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			ActionTable<char> actionTable;

			actionTable = new MockedActionTable("A", "B", "C");

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 1 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 2 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextNonTerminals(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(3, distinctInputs.Length);
			Assert.AreEqual("A", distinctInputs[0]);
			Assert.AreEqual("B", distinctInputs[1]);
			Assert.AreEqual("C", distinctInputs[2]);
		}





		[TestMethod]
		public void ShouldGetNextSituations()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			ActionTable<char> actionTable;
			Situation<char>[] nextSituations;

			actionTable = new MockedActionTable('a', 'b', 'c');

			s1 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s2 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };
			s3 = new Situation<char>() { ActionTable = actionTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			nextSituations = producer.GetNextSituations(new Situation<char>[] { s1, s2, s3 }, 'a').ToArray();

			Assert.AreEqual(1, nextSituations.Length);
			Assert.AreEqual(1, nextSituations[0].StateIndex);
		}
		


	}
}
