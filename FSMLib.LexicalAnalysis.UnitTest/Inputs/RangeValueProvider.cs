using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Inputs;

namespace FSMLib.LexicalAnalysis.UnitTest.Inputs
{
	
	[TestClass]
	public class RangeValueProviderUnitTest
	{

		[TestMethod]
		public void ShoudReturnIsMinValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.IsTrue(provider.IsMinValue(char.MinValue));
			Assert.IsFalse(provider.IsMinValue('a'));
		}

		[TestMethod]
		public void ShoudReturnIsMaxValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.IsTrue(provider.IsMaxValue(char.MaxValue));
			Assert.IsFalse(provider.IsMaxValue('a'));
		}

		[TestMethod]
		public void ShoudReturnNextValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.AreEqual('b', provider.GetNextValue('a'));
		}
		[TestMethod]
		public void ShoudReturnPreviousValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.AreEqual('a', provider.GetPreviousValue('b'));
		}

		[TestMethod]
		public void ShoudNotReturnNextValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.ThrowsException<ArgumentOutOfRangeException>(()=> provider.GetNextValue(char.MaxValue));
		}
		[TestMethod]
		public void ShoudNotReturnPreviousValue()
		{
			RangeValueProvider provider;

			provider = new RangeValueProvider();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => provider.GetPreviousValue(char.MinValue));
		}
		[TestMethod]
		public void ShoudNReturnTerminalRange()
		{
			RangeValueProvider provider;
			ITerminalRangeInput<char> range;

			provider = new RangeValueProvider();
			range = provider.CreateTerminalRangeInput('a', 'z');
			Assert.AreEqual('a', range.FirstValue);
			Assert.AreEqual('z', range.LastValue);
		}

	}
}
