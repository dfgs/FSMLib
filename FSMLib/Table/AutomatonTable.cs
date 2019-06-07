using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class AutomatonTable<T> 
	{
		public List<State<T>> States
		{
			get;
			set;
		}

		/*public List<T> Alphabet
		{
			get;
			set;
		}*/

		public AutomatonTable()
		{
			States = new List<State<T>>();
			//Alphabet = new List<T>();
		}

		
	}
}
