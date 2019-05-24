using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class ActionTable<T> 
	{
		public List<State<T>> States
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
			States = new List<State<T>>();
			Alphabet = new List<T>();
		}

		
	}
}
