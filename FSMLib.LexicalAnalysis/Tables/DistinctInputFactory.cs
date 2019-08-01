using FSMLib.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib;

namespace FSMLib.LexicalAnalysis.Tables
{
	public class DistinctInputFactory : IDistinctInputFactory<char>
	{
		public IEnumerable<IActionInput<char>> GetDistinctInputs(IEnumerable<IInput<char>> Inputs)
		{
			return Inputs.DistinctEx().OfType<IActionInput<char>>();
		}
	}
}
