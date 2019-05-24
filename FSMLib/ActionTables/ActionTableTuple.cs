using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class ActionTableTuple<T>
	{
		public Node<T> Node
		{
			get;
			set;
		}
		public IEnumerable<Situation<T>> Situations
		{
			get;
			set;
		}

		public ActionTableTuple()
		{

		}
		public ActionTableTuple(Node<T> Node, IEnumerable<Situation<T>> Situations)
		{
			if (Node == null) throw new ArgumentNullException();
			if (Situations == null) throw new ArgumentNullException();
			this.Node = Node;this.Situations = Situations;
		}
	}
}
