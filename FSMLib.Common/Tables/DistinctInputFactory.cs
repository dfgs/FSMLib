using FSMLib.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib;
using FSMLib.Common.Inputs;
using FSMLib.Common;

namespace FSMLib.Common.Tables
{
	public abstract class DistinctInputFactory<T> : IDistinctInputFactory<T>
		where T:IComparable<T>
	{
		private IRangeValueProvider<T> provider;

		protected abstract ITerminalInput<T> CreateTerminalInput(T Value);

		public DistinctInputFactory(IRangeValueProvider<T> RangeValueProvider)
		{
			if (RangeValueProvider == null) throw new ArgumentNullException("RangeValueProvider");
			this.provider = RangeValueProvider;
		}

		public IEnumerable<IActionInput<T>> GetDistinctInputs(IEnumerable<IInput<T>> Inputs)
		{
			List<IActionInput<T>> results;
			TerminalRangeInputCollection<T> items;

			results = new List<IActionInput<T>>();
			items = new TerminalRangeInputCollection<T>(provider);
			foreach(IInput<T> input in Inputs)
			{
				switch(input)
				{
					case ITerminalInput<T> terminalInput:
						items.Add(terminalInput.Value);
						break;
					case ITerminalRangeInput<T> terminalRange:
						items.Add(terminalRange);
						break;
					case IActionInput<T> i:
						if (results.FirstOrDefault(item=>item.Equals(i))==null) results.Add(i);
						break;
				}
			}

			// return results, and convert one item ranges to single terminal
			foreach (ITerminalRangeInput<T> input in items)
			{
				if (input.FirstValue.Equals(input.LastValue)) yield return CreateTerminalInput(input.FirstValue);
				else yield return input;
			}
			foreach(IActionInput<T> input in results)
			{
				yield return input;
			}

		}
	}
}
