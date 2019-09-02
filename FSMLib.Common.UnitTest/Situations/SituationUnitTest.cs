using System;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Situations;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Common.Situations;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Situations
{

	[TestClass]
	public class SituationUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Situation<char>(null, new MockedPredicate(), new MockedReduceInput()));
			Assert.ThrowsException<ArgumentNullException>(() => new Situation<char>(new MockedRule(), null, new MockedReduceInput()));
			Assert.ThrowsException<ArgumentNullException>(() => new Situation<char>(new MockedRule(), new MockedPredicate(), null));
		}
		[TestMethod]
		public void ShouldBeEquals()
		{
			Situation<char> a, b;

			a = new Situation<char>(new MockedRule(),new MockedPredicate(),new MockedReduceInput());
			b = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotBeEquals()
		{
			Situation<char> a, b;

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule2(), new MockedPredicate(), new MockedReduceInput());
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule(), new MockedPredicate2(), new MockedReduceInput());
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));

			a = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput());
			b = new Situation<char>(new MockedRule(), new MockedPredicate(), new MockedReduceInput2());
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));

		}



	}
}
