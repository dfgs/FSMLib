using FSMLib.Graphs.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Node<T>
	{
		public string Name
		{
			get;
			set;
		}
		public List<TerminalTransition<T>> TerminalTransitions
		{
			get;
			set;
		}
		public List<NonTerminalTransition<T>> NonTerminalTransitions
		{
			get;
			set;
		}

		public List<ReductionTransition<T>> ReductionTransitions
		{
			get;
			set;
		}

		public List<AcceptTransition<T>> AcceptTransitions
		{
			get;
			set;
		}

		/*public List<int> RootIDs
		{
			get;
			set;
		}*/

		public Node()
		{
			TerminalTransitions = new List<TerminalTransition<T>>();
			NonTerminalTransitions = new List<NonTerminalTransition<T>>();
			ReductionTransitions = new List<ReductionTransition<T>>();
			AcceptTransitions = new List<AcceptTransition<T>>();
			//RootIDs = new List<int>();
		}
		public override string ToString()
		{
			return Name??"No name";
		}

	}
}
