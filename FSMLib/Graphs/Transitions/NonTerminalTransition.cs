
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Transitions
{
	public class NonTerminalTransition<T>:ShiftTransition<T>
	{

		
		public string Name
		{
			get;
			set;
		}

		// todo: add unit test
		public bool Match(string Other)
		{
			return Name == Other;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
