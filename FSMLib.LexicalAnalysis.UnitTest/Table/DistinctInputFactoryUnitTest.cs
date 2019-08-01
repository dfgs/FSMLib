using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Tables;
using FSMLib.Rules;
using FSMLib.Situations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.UnitTest.Table
{
	[TestClass]
	public class DistinctInputFactoryUnitTest
	{

		[TestMethod]

		public void ShouldGetDistinctOneNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new LetterInput('a'), new LetterInput('a'), new LetterInput('a') }).ToArray();

			Assert.AreEqual(1, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
		}

		[TestMethod]

		public void ShouldGetDistinctTwoNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new LetterInput('a'), new LetterInput('a'), new LetterInput('b') }).ToArray();

			Assert.AreEqual(2, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
			Assert.IsTrue(inputs[1].Match('b'));
		}


		public void ShouldGetDistinctThreeNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new LetterInput('a'), new LetterInput('b'), new LetterInput('c') }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.IsTrue(inputs[0].Match('a'));
			Assert.IsTrue(inputs[1].Match('b'));
			Assert.IsTrue(inputs[2].Match('c'));
		}


	}
}
