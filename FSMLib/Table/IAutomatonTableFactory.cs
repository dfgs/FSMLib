using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IAutomatonTableFactory<T>
	{
		AutomatonTable<T> BuildAutomatonTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet);
		AutomatonTable<T> BuildDeterministicAutomatonTable(AutomatonTable<T> BaseAutomatonTable);

	}
}
