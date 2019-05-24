﻿using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Helpers;
using FSMLib.Rules;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;

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
			BaseTerminalInput<char>[] distinctInputs;
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable('a','b','c');

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs=producer.GetNextTerminalInputs(new Situation<char>[] { s1,s2,s3 }).ToArray();

			Assert.AreEqual(1, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			

		}
		[TestMethod]
		public void ShouldGetTwoDistinctTerminals()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			BaseTerminalInput<char>[] distinctInputs;
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable('a', 'b', 'c');

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 1 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminalInputs(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(2, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));

		}

		[TestMethod]
		public void ShouldGetThreeDistinctTerminals()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			BaseTerminalInput<char>[] distinctInputs;
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable('a', 'b', 'c');

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 1 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 2 };

			producer = new SituationProducer<char>();
			distinctInputs = producer.GetNextTerminalInputs(new Situation<char>[] { s1, s2, s3 }).ToArray();

			Assert.AreEqual(3, distinctInputs.Length);
			Assert.IsTrue(distinctInputs[0].Match('a'));
			Assert.IsTrue(distinctInputs[1].Match('b'));
			Assert.IsTrue(distinctInputs[2].Match('c'));

		
		}





		[TestMethod]
		public void ShouldGetOneDistinctNonTerminal()
		{
			Situation<char> s1, s2, s3;
			SituationProducer<char> producer;
			string[] distinctInputs;
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable("A", "B", "C");

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };

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
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable("A", "B", "C");

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 1 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };

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
			AutomatonTable<char> automatonTable;

			automatonTable = new MockedAutomatonTable("A", "B", "C");

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 1 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 2 };

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
			AutomatonTable<char> automatonTable;
			Situation<char>[] nextSituations;

			automatonTable = new MockedAutomatonTable('a', 'b', 'c');

			s1 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s2 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };
			s3 = new Situation<char>() { AutomatonTable = automatonTable, StateIndex = 0 };

			producer = new SituationProducer<char>();
			nextSituations = producer.GetNextSituations(new Situation<char>[] { s1, s2, s3 }, new TerminalInput<char>() { Value = 'a' }).ToArray();

			Assert.AreEqual(1, nextSituations.Length);
			Assert.AreEqual(1, nextSituations[0].StateIndex);
		}
		


	}
}
