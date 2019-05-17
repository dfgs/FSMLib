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
		public List<Transition<T>> Transitions
		{
			get;
			set;
		}

		public List<MatchedRule> MatchedRules
		{
			get;
			set;
		}

		public List<int> RootIDs
		{
			get;
			set;
		}

		public Node()
		{
			Transitions = new List<Transition<T>>();
			MatchedRules = new List<MatchedRule>();
			RootIDs = new List<int>();
		}
		public override string ToString()
		{
			return Name??"No name";
		}

	}
}
