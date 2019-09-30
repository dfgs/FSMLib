using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public interface IRangeValueProvider<T>
	{
		ITerminalRangeInput<T> CreateTerminalRangeInput(T FirstValue, T LastValue);
		T GetPreviousValue(T Value);
		T GetNextValue(T Value);
		bool IsMinValue(T Value);
		bool IsMaxValue(T Value);

	}
}
