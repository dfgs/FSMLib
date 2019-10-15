using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis.Tables;
using FSMLib.Rules;
using FSMLib.Situations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.SyntaxicAnalysis;

namespace FSMLib.SyntaxicAnalysis.UnitTest.Tables
{
	[TestClass]
	public class DistinctInputFactoryUnitTest
	{

		[TestMethod]

		public void ShouldGetDistinctOneNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<Token>[] inputs;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<Token>[] { new TerminalInput(new Token("C","a")), new TerminalInput(new Token("C","a")), new TerminalInput(new Token("C","a")) }).ToArray();

			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual(new Token("C","a"), ((ITerminalInput<Token>)inputs[0]).Value );
		}

		[TestMethod]

		public void ShouldGetDistinctTwoNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<Token>[] inputs;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<Token>[] { new TerminalInput(new Token("C","a")), new TerminalInput(new Token("C","a")), new TerminalInput(new Token("C", "b")) }).ToArray();

			Assert.AreEqual(2, inputs.Length);
			Assert.AreEqual(new Token("C","a"), ((ITerminalInput<Token>)inputs[0]).Value);
			Assert.AreEqual(new Token("C", "b"), ((ITerminalInput<Token>)inputs[1]).Value);
		}


		[TestMethod]
		public void ShouldGetDistinctThreeNextInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<Token>[] inputs;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<Token>[] { new TerminalInput(new Token("C","a")), new TerminalInput(new Token("C", "b")), new TerminalInput(new Token("C", "c")) }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual(new Token("C","a"), ((ITerminalInput<Token>)inputs[0]).Value);
			Assert.AreEqual(new Token("C", "b"), ((ITerminalInput<Token>)inputs[1]).Value);
			Assert.AreEqual(new Token("C", "c"), ((ITerminalInput<Token>)inputs[2]).Value);
		}
		[TestMethod]
		public void ShouldGetDistinctOneNonTerminalInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<Token>[] inputs;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<Token>[] { new NonTerminalInput("A"), new NonTerminalInput("A"), new NonTerminalInput("A") }).ToArray();

			Assert.AreEqual(1, inputs.Length);
			Assert.AreEqual("A", ((INonTerminalInput<Token>)inputs[0]).Name);
		}
		[TestMethod]
		public void ShouldGetDistinctThreeNonTerminalInputs()
		{
			DistinctInputFactory distinctInputFactory;
			IActionInput<Token>[] inputs;

			distinctInputFactory = new DistinctInputFactory(new RangeValueProvider());
			inputs = distinctInputFactory.GetDistinctInputs(new IInput<Token>[] { new NonTerminalInput("A"), new NonTerminalInput("B"), new NonTerminalInput("C") }).ToArray();

			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual("A", ((INonTerminalInput<Token>)inputs[0]).Name);
			Assert.AreEqual("B", ((INonTerminalInput<Token>)inputs[1]).Name);
			Assert.AreEqual("C", ((INonTerminalInput<Token>)inputs[2]).Name);
		}

		
		


	}
}
