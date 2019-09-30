using FSMLib.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common;

namespace FSMLib.LexicalAnalysis.Tables
{
	public class DistinctInputFactory : IDistinctInputFactory<char>
	{
		public IEnumerable<IActionInput<char>> GetDistinctInputs(IEnumerable<IInput<char>> Inputs)
		{
			List<IActionInput<char>> results;
			TerminalRangeInputCollection<char> items;

			results = new List<IActionInput<char>>();
			items = new TerminalRangeInputCollection<char>(new RangeValueProvider());
			foreach(IInput<char> input in Inputs)
			{
				switch(input)
				{
					case TerminalInput letterInput:
						items.Add(letterInput.Value);
						break;
					case TerminalRangeInput lettersRange:
						items.Add(lettersRange);
						break;
					case IActionInput<char> i:
						if (results.FirstOrDefault(item=>item.Equals(i))==null) results.Add(i);
						break;
				}
			}

			// return results, and convert one item ranges to single terminal
			foreach (TerminalRangeInput input in items)
			{
				if (input.FirstValue == input.LastValue) yield return new TerminalInput(input.FirstValue);
				else yield return input;
			}
			foreach(IActionInput<char> input in results)
			{
				yield return input;
			}

		}
	}
}
