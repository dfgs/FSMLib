using FSMLib.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib;
using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.SyntaxicAnalysis;
using FSMLib.Common.Tables;

namespace FSMLib.SyntaxicAnalysis.Tables
{
	public class DistinctInputFactory : DistinctInputFactory<Token>
	{
		public DistinctInputFactory(IRangeValueProvider<Token> RangeValueProvider) : base(RangeValueProvider)
		{

		}
		protected override ITerminalInput<Token> CreateTerminalInput(Token Value)
		{
			return new TerminalInput(Value);
		}


	}
}
