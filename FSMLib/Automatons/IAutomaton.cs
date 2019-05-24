
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Tables
{
	public interface IAutomaton<T>
	{
		int StackCount
		{
			get;
		}

		void Reset();

		bool CanFeed(BaseTerminalInput<T> Input);
		void Feed(BaseTerminalInput<T> Input);

		bool CanAccept();

		NonTerminalNode<T> Accept();

	}
}
