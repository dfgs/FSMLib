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

namespace FSMLib.LexicalAnalysis.UnitTest.Tables
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
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new TerminalInput('a'), new TerminalInput('a'), new TerminalInput('a') }).ToArray();

			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual('a', ((ITerminalInput<char>)inputs[0]).Value );
		}

		[TestMethod]

		public void ShouldGetDistinctTwoNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new TerminalInput('a'), new TerminalInput('a'), new TerminalInput('b') }).ToArray();

			Assert.AreEqual(2, inputs.Length);
			Assert.AreEqual('a', ((ITerminalInput<char>)inputs[0]).Value);
			Assert.AreEqual('b', ((ITerminalInput<char>)inputs[1]).Value);
		}


		[TestMethod]
		public void ShouldGetDistinctThreeNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new TerminalInput('a'), new TerminalInput('b'), new TerminalInput('c') }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', ((ITerminalInput<char>)inputs[0]).Value);
			Assert.AreEqual('b', ((ITerminalInput<char>)inputs[1]).Value);
			Assert.AreEqual('c', ((ITerminalInput<char>)inputs[2]).Value);
		}
		[TestMethod]
		public void ShouldGetDistinctOneNonTerminalInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new NonTerminalInput("A"), new NonTerminalInput("A"), new NonTerminalInput("A") }).ToArray();

			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual("A", ((INonTerminalInput<char>)inputs[0]).Name);
		}
		[TestMethod]
		public void ShouldGetDistinctThreeNonTerminalInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new NonTerminalInput("A"), new NonTerminalInput("B"), new NonTerminalInput("C") }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual("A", ((INonTerminalInput<char>)inputs[0]).Name);
			Assert.AreEqual("B", ((INonTerminalInput<char>)inputs[1]).Name);
			Assert.AreEqual("C", ((INonTerminalInput<char>)inputs[2]).Name);
		}

		[TestMethod]
		public void ShouldSplitLettersRanges()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new TerminalRangeInput('a','d'), new TerminalRangeInput('c','f')}).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('b', ((TerminalRangeInput)inputs[0]).LastValue);
			Assert.AreEqual('c', ((TerminalRangeInput)inputs[1]).FirstValue);
			Assert.AreEqual('d', ((TerminalRangeInput)inputs[1]).LastValue);
			Assert.AreEqual('e', ((TerminalRangeInput)inputs[2]).FirstValue);
			Assert.AreEqual('f', ((TerminalRangeInput)inputs[2]).LastValue);
		}
		[TestMethod]
		public void ShouldSplitLettersRangesUsingOneLetter()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<char>[] inputs;

			distinctInputFactory = new DistinctInputFactory();
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<char>[] { new TerminalRangeInput('a', 'g'), new TerminalInput('c') }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', ((TerminalRangeInput)inputs[0]).FirstValue);
			Assert.AreEqual('b', ((TerminalRangeInput)inputs[0]).LastValue);
			Assert.AreEqual('c', ((TerminalInput)inputs[1]).Value);
			Assert.AreEqual('d', ((TerminalRangeInput)inputs[2]).FirstValue);
			Assert.AreEqual('g', ((TerminalRangeInput)inputs[2]).LastValue);
		}
		


	}
}
