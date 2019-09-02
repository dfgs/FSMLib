using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Table;
using FSMLib.Inputs;
using FSMLib.Common.Situations;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Situations
{
	
	[TestClass]
	public class SituationCollectionUnitTest
	{
		

		
		[TestMethod]

		public void ShouldAdd()
		{
			Situation<char> a,b;
			SituationCollection<char> situations;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());

			situations = new SituationCollection<char>();
			Assert.AreEqual(0, situations.Count);
			situations.Add(a);
			Assert.AreEqual(1, situations.Count);
			situations.Add(b);
			Assert.AreEqual(2, situations.Count);
		}

		[TestMethod]

		public void ShouldNotAddDuplicates()
		{
			Situation<char> a, b,c;
			SituationCollection<char> situations;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());
			c = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());

			situations = new SituationCollection<char>();
			Assert.AreEqual(0, situations.Count);
			situations.Add(a);
			Assert.AreEqual(1, situations.Count);
			situations.Add(b);
			Assert.AreEqual(2, situations.Count);
			situations.Add(c);
			Assert.AreEqual(2, situations.Count);
		}

		[TestMethod]

		public void ShouldContains()
		{
			Situation<char> a, b, c;
			SituationCollection<char> situations;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());
			c = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput());

			situations = new SituationCollection<char>();
			situations.Add(a);
			situations.Add(b);

			Assert.IsTrue(situations.Contains(a));
			Assert.IsTrue(situations.Contains(b));
			Assert.IsFalse(situations.Contains(c));
		}
		[TestMethod]

		public void ShouldEquals()
		{
			Situation<char> a, b, c;
			SituationCollection<char> situations1,situations2;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());
			c = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput());

			situations1 = new SituationCollection<char>();
			situations1.Add(a);
			situations1.Add(b);
			situations1.Add(c);

			situations2 = new SituationCollection<char>();	// different order
			situations2.Add(c);
			situations2.Add(a);
			situations2.Add(b);

			Assert.IsTrue(situations1.Equals(situations2));
			Assert.IsTrue(situations2.Equals(situations1));
		}

		[TestMethod]

		public void ShouldNotEquals()
		{
			Situation<char> a, b, c;
			SituationCollection<char> situations1, situations2;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput2());
			c = new Situation<char>(new MockedRule2(), new MockedPredicate2(), new MockedReduceInput());

			situations1 = new SituationCollection<char>();
			situations1.Add(a);
			situations1.Add(b);
			situations1.Add(c);

			situations2 = new SituationCollection<char>();  // different order
			situations2.Add(c);
			situations2.Add(b);

			Assert.IsFalse(situations1.Equals(situations2));
			Assert.IsFalse(situations2.Equals(situations1));
		}

		[TestMethod]

		public void ShouldGetReductionSituations()
		{
			Situation<char> a, b;
			SituationCollection<char> situations;
			ISituation<char>[] reductions;
	

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions =situations.GetReductionSituations().ToArray();
			Assert.AreEqual(1, reductions.Length);


			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate(), new MockedReduceInput());

			situations = new SituationCollection<char>();
			situations.Add(a); situations.Add(b);
			reductions = situations.GetReductionSituations().ToArray();
			Assert.AreEqual(2, reductions.Length);
		}

	


	}
}
