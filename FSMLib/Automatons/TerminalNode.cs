using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class TerminalNode<T>:BaseNode<T>
	{
		public T Value
		{
			get;
			set;
		}

		

	}
}
