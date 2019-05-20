using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public interface IAutomaton<T>
	{
		int StackCount
		{
			get;
		}

		void Reset();

		bool Feed(IInput<T> Item);
		bool Feed(T Item);

		bool CanReduce();

		NonTerminalNode<T> Reduce();

		void SaveSituation();
		void RestoreSituation();
	}
}
