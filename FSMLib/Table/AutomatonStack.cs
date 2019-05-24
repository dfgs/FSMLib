using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class AutomatonStack<T>:List<T>
	{
		public void Push(T Item)
		{
			Add(Item);
		}
		public T Pop()
		{
			T item;
			item = this[Count - 1];
			RemoveAt(Count - 1);
			return item; 
		}


	}
}
