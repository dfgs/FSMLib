
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

		//bool Feed(IInput<T> Item);
		void Feed(T Input);

		bool CanAccept();

		NonTerminalNode<T> Accept();

		/*void SaveSituation();
		void RestoreSituation();*/
	}
}
