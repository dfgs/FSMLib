using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public interface IActionTableFactory<T>
	{
		ActionTable<T> BuildActionTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet);
		ActionTable<T> BuildDeterministicActionTable(ActionTable<T> BaseActionTable);

	}
}
