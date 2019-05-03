using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public interface IAutomaton<T>
	{
		void Reset();

		bool Feed(T Item);

		bool CanReduce();

		string Reduce();
	}
}
