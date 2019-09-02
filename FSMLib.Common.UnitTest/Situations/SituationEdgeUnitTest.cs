using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Predicates;
using System.Linq;
using FSMLib.Rules;
using FSMLib.Table;
using FSMLib.Common.Situations;
using FSMLib.Common.Table;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.Situations
{
	
	[TestClass]
	public class SituationEdgeUnitTest
	{
		

		
		[TestMethod]

		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SituationEdge<char>(null, new MockedPredicate(), new SituationNode<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SituationEdge<char>(new MockedRule(), null, new SituationNode<char>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SituationEdge<char>(new MockedRule(), new MockedPredicate(), null));
		}




	}
}
