using System;
using FSMLib.Graphs.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Graphs.Inputs
{
	[TestClass]
	public class GenericInputUnitTest
	{
		[TestMethod]
		public void ShouldHaveCorrectPriorities()
		{
			Assert.IsTrue(new OneInput<char>().Priority < new AnyInput<char>().Priority);
		}
	}
}
