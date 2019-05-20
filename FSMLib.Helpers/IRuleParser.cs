using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
	public interface IRuleParser<T>
	{
		Rule<T> Parse(string Rule);
	}
}
