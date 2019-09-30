using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.LexicalAnalysis.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest
{
	[TestClass]
	public class TerminalRangeInputCollectionUnitTest
	{
		[TestMethod]

		public void ShouldHaveCorrectConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new TerminalRangeInputCollection<char>(null));
		}

		[TestMethod]

		public void ShouldAddLetterInput()
		{
			TerminalRangeInputCollection<char> items;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add('a');
			Assert.AreEqual(1, items.Count);
			items.Add('b');
			Assert.AreEqual(2, items.Count);
		}
		[TestMethod]

		public void ShouldAddLettersRangeInput()
		{
			TerminalRangeInputCollection<char> items;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a','b'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('c','d'));
			Assert.AreEqual(2, items.Count);
		}
		[TestMethod]

		public void ShouldSplitLettersRangeInputsLeftCase()
		{
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('d', 'h'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('a', 'f'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a', 'f'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('d', 'h'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a', 'f'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('c', 'd'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('c', 'd'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('a', 'f'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a', 'c'));
			Assert.AreEqual(1, items.Count);
			items.Add(new TerminalRangeInput('e', 'g'));
			Assert.AreEqual(2, items.Count);
			items.Add(new TerminalRangeInput('c', 'e'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a', 'g'));
			items.Add('c');
	
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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] inputs;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			Assert.AreEqual(0, items.Count);
			items.Add(new TerminalRangeInput('a', 'g'));
			items.Add('c');
			items.Add('c');
			items.Add(new TerminalRangeInput('a', 'b'));
			items.Add(new TerminalRangeInput('d', 'g'));

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
			TerminalRangeInputCollection<char> items;
			ITerminalRangeInput<char>[] ranges;

			items = new TerminalRangeInputCollection<char>(new FSMLib.LexicalAnalysis.RangeValueProvider());

			items.Add('a');
			items.Add('b');

			ranges = items.ToArray();
			Assert.AreEqual(2, ranges.Length);
			Assert.AreEqual('a', ranges[0].FirstValue);
			Assert.AreEqual('a', ranges[0].LastValue);
			Assert.AreEqual('b', ranges[1].FirstValue);
			Assert.AreEqual('b', ranges[1].LastValue);
		}




	}
}
