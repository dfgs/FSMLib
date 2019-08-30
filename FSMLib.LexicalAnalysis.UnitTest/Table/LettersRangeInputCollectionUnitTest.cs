using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.UnitTest.Table
{
	[TestClass]
	public class LettersRangeInputCollectionUnitTest
	{
		[TestMethod]

		public void ShouldAddLetterInput()
		{
			LettersRangeInputCollection items;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalInput('a'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalInput('b'));
			Assert.AreEqual(2, items.Count);
		}
		[TestMethod]

		public void ShouldAddLettersRangeInput()
		{
			LettersRangeInputCollection items;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a','b'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('c','d'));
			Assert.AreEqual(2, items.Count);
		}
		[TestMethod]

		public void ShouldSplitLettersRangeInputsLeftCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('d', 'h'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('a', 'f'));

			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('c', inputs[0].LastValue);
			Assert.AreEqual('d', inputs[1].FirstValue);
			Assert.AreEqual('f', inputs[1].LastValue);
			Assert.AreEqual('g', inputs[2].FirstValue);
			Assert.AreEqual('h', inputs[2].LastValue);
		}
		[TestMethod]
		public void ShouldSplitLettersRangeInputsRightCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a', 'f'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('d', 'h'));

			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('c', inputs[0].LastValue);
			Assert.AreEqual('d', inputs[1].FirstValue);
			Assert.AreEqual('f', inputs[1].LastValue);
			Assert.AreEqual('g', inputs[2].FirstValue);
			Assert.AreEqual('h', inputs[2].LastValue);
		}

		[TestMethod]
		public void ShouldSplitLettersRangeInputsEnglobedCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a', 'f'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('c', 'd'));

			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('b', inputs[0].LastValue);
			Assert.AreEqual('c', inputs[1].FirstValue);
			Assert.AreEqual('d', inputs[1].LastValue);
			Assert.AreEqual('e', inputs[2].FirstValue);
			Assert.AreEqual('f', inputs[2].LastValue);
		}
		[TestMethod]
		public void ShouldSplitLettersRangeInputsEnglobingCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('c', 'd'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('a', 'f'));

			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('b', inputs[0].LastValue);
			Assert.AreEqual('c', inputs[1].FirstValue);
			Assert.AreEqual('d', inputs[1].LastValue);
			Assert.AreEqual('e', inputs[2].FirstValue);
			Assert.AreEqual('f', inputs[2].LastValue);
		}

		[TestMethod]
		public void ShouldSplitLettersRangeInputsIntersectsSeveralSegmentsCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a', 'c'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalsRangeInput('e', 'g'));
			Assert.AreEqual(2, items.Count);
			items.Add(new TerminalsRangeInput('c', 'e'));

			inputs = items.ToArray();
			Assert.AreEqual(5, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('b', inputs[0].LastValue);
			Assert.AreEqual('c', inputs[1].FirstValue);
			Assert.AreEqual('c', inputs[1].LastValue);
			Assert.AreEqual('d', inputs[2].FirstValue);
			Assert.AreEqual('d', inputs[2].LastValue);
			Assert.AreEqual('e', inputs[3].FirstValue);
			Assert.AreEqual('e', inputs[3].LastValue);
			Assert.AreEqual('f', inputs[4].FirstValue);
			Assert.AreEqual('g', inputs[4].LastValue);
		}
		[TestMethod]
		public void ShouldSplitLettersRangeInputsUsingSingleTerminalCase()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a', 'g'));
			items.Add(new TerminalInput('c'));
	
			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('b', inputs[0].LastValue);
			Assert.AreEqual('c', inputs[1].FirstValue);
			Assert.AreEqual('c', inputs[1].LastValue);
			Assert.AreEqual('d', inputs[2].FirstValue);
			Assert.AreEqual('g', inputs[2].LastValue);
			
		}
		[TestMethod]
		public void ShouldNotSplitLettersRangeInputsWhenAddingExistingRange()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] inputs;

			items = new LettersRangeInputCollection();

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalsRangeInput('a', 'g'));
			items.Add(new TerminalInput('c'));
			items.Add(new TerminalInput('c'));
			items.Add(new TerminalsRangeInput('a', 'b'));
			items.Add(new TerminalsRangeInput('d', 'g'));

			inputs = items.ToArray();
			Assert.AreEqual(3, inputs.Length);
			Assert.AreEqual('a', inputs[0].FirstValue);
			Assert.AreEqual('b', inputs[0].LastValue);
			Assert.AreEqual('c', inputs[1].FirstValue);
			Assert.AreEqual('c', inputs[1].LastValue);
			Assert.AreEqual('d', inputs[2].FirstValue);
			Assert.AreEqual('g', inputs[2].LastValue);

		}
		[TestMethod]

		public void ShouldEnumerate()
		{
			LettersRangeInputCollection items;
			TerminalsRangeInput[] ranges;

			items = new LettersRangeInputCollection();

			items.Add(new TerminalInput('a'));
			items.Add(new TerminalInput('b'));

			ranges = items.ToArray();
			Assert.AreEqual(2, ranges.Length);
			Assert.AreEqual('a', ranges[0].FirstValue);
			Assert.AreEqual('a', ranges[0].LastValue);
			Assert.AreEqual('b', ranges[1].FirstValue);
			Assert.AreEqual('b', ranges[1].LastValue);
		}




	}
}
