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
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.Tables
{
	public class DistinctInputFactory : IDistinctInputFactory<Token>
	{
		public IEnumerable<IActionInput<Token>> GetDistinctInputs(IEnumerable<IInput<Token>> Inputs)
		{
			List<IActionInput<Token>> items;
			IActionInput<Token> existing;

			items = new List<IActionInput<Token>>();

			foreach(IActionInput<Token> item in Inputs.OfType<IActionInput<Token>>())
			{
				existing = items.FirstOrDefault(i => i.Equals(item));
				if (existing != null) continue;
				items.Add(item);
			}

			return items;
		}

	}
}
