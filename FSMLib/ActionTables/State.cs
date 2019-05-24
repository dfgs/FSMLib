using FSMLib.ActionTables.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class State<T>
	{
		/*public string Name
		{
			get;
			set;
		}*/
		public List<ShiftOnTerminal<T>> TerminalActions
		{
			get;
			set;
		}
		public List<ShifOnNonTerminal<T>> NonTerminalActions
		{
			get;
			set;
		}

		public List<Reduce<T>> ReductionActions
		{
			get;
			set;
		}

		public List<Accept<T>> AcceptActions
		{
			get;
			set;
		}

		

		public State()
		{
			TerminalActions = new List<ShiftOnTerminal<T>>();
			NonTerminalActions = new List<ShifOnNonTerminal<T>>();
			ReductionActions = new List<Reduce<T>>();
			AcceptActions = new List<Accept<T>>();
			//RootIDs = new List<int>();
		}
		/*public override string ToString()
		{
			return Name??"No name";
		}*/

	}
}
