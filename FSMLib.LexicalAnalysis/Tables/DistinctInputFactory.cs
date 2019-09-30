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
using FSMLib.Common.Tables;

namespace FSMLib.LexicalAnalysis.Tables
{
	public class DistinctInputFactory : DistinctInputFactory<char>
	{
		public DistinctInputFactory(IRangeValueProvider<char> RangeValueProvider):base(RangeValueProvider)
		{

		}
		protected override ITerminalInput<char> CreateTerminalInput(char Value)
		{
			return new TerminalInput(Value);
		}
		

	}
}
