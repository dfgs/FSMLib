using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class ActionTable<T> 
	{
		public List<Node<T>> Nodes
		{
			get;
			set;
		}

		public List<T> Alphabet
		{
			get;
			set;
		}

		public ActionTable()
		{
			Nodes = new List<Node<T>>();
			Alphabet = new List<T>();
		}

		
	}
}
